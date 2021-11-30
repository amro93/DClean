using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.Interfaces.Repositories;
using DClean.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;
using System.Reflection;
using DClean.Application.Interfaces.Identity;
using DClean.Infrastructure.Persistence.Contexts;

namespace DClean.Infrastructure.Common.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class, IEntity
    {
        protected readonly DbSet<T> Set;
        private readonly DbContext dbContext;
        private readonly ILogger logger;
        private readonly Guid? _currentUserId;
        private readonly ICurrentTenant _currentTenant;
        public BaseRepository(IDCleanDbContextFactory dbContextFactory,
            IServiceProvider serviceProvider
            )
        {
            this.dbContext = dbContextFactory.CreateDbContext();
            Set = dbContext.Set<T>();
            var currentUser = serviceProvider.GetRequiredService<ICurrentUser>();
            _currentTenant = serviceProvider.GetRequiredService<ICurrentTenant>();
            this.logger = serviceProvider.GetRequiredService<ILoggerFactory>()?.CreateLogger("BaseRepository");
            _currentUserId = currentUser.UserId;
        }
        public virtual void Create(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            Set.Add(entity);
        }

        public virtual async Task CreateAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await Set.AddAsync(entity);
        }

        public virtual void CreateMany(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            Set.AddRange(entities);
        }

        public virtual async Task CreateManyAsync(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await Set.AddRangeAsync(entities);
        }

        public virtual void PhysicalDelete(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                Set.Attach(entity);
            }
            Set.Remove(entity);
        }

        public virtual void PhysicalDeleteMany(IEnumerable<T> entities)
        {
            Set.AttachRange(entities.Where(e => dbContext.Entry(e).State == EntityState.Detached));
            Set.RemoveRange(entities);
        }

        protected virtual IQueryable<T> GetQuerable()
        {
            return Set.AsNoTracking().AsQueryable();
        }

        public virtual IQueryable<T> GetTable()
        {
            var query = GetQuerable().AsNoTracking();
            return query;
        }

        public virtual int Save()
        {
            return dbContext.SaveChanges();
        }
        public virtual async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            Set.Update(entity);
        }
        public virtual void UpdateMany(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            Set.UpdateRange(entities);
        }
        public virtual void Delete(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var softDeletable = ParseToSoftDeleteable(entity);
            if (softDeletable == null) throw new DbUpdateException($"The entity of type {typeof(T).Name} doesn't implement the interface {nameof(ISoftDeleteEntity)}");

            softDeletable.IsDeleted = true;

            Update(softDeletable as T);
        }
        private ISoftDeleteEntity ParseToSoftDeleteable(T entity)
        {
            if (entity is ISoftDeleteEntity) return entity as ISoftDeleteEntity;

            return null;
        }

        private IEnumerable<ISoftDeleteEntity> ParseForSoftDelete(IEnumerable<T> entities)
        {
            if (entities is IEnumerable<ISoftDeleteEntity>) return entities as IEnumerable<ISoftDeleteEntity>;

            return null;
        }

        public virtual void DeleteMany(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            var softDeletable = ParseForSoftDelete(entities);
            if (softDeletable == null) throw new DbUpdateException($"The entity of type {typeof(T).Name} doesn't implement the interface {nameof(ISoftDeleteEntity)}");

            foreach (var entity in softDeletable)
            {
                entity.IsDeleted = true;
            }

            UpdateMany(softDeletable as IEnumerable<T>);
        }

    }
    public class BaseRepository<T, TPK> : BaseRepository<T>, IBaseRepository<T, TPK> where T : class, IEntity<TPK>
        where TPK : IEquatable<TPK>
    {
        private readonly DbContext dbContext;
        private readonly ILogger logger;
        private readonly Guid? _currentUserId;
        private readonly ICurrentTenant _currentTenant;

        public BaseRepository(IDCleanDbContextFactory dbContextFactory,
            IServiceProvider serviceProvider
            ) : base(dbContextFactory, serviceProvider)
        {
            this.dbContext = dbContextFactory.CreateDbContext();
            var currentUser = serviceProvider.GetRequiredService<ICurrentUser>();
            _currentTenant = serviceProvider.GetRequiredService<ICurrentTenant>();
            this.logger = serviceProvider.GetRequiredService<ILoggerFactory>()?.CreateLogger("BaseRepository");
            _currentUserId = currentUser.UserId;
        }

        public virtual T GetById(TPK id)
        {
            return Set.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(TPK id)
        {
            return await Set.FindAsync(id);
        }

        public virtual void PhysicalDelete(TPK id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                PhysicalDelete(entity);
            }
        }

        public virtual async Task PhysicalDeleteAsync(TPK id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                PhysicalDelete(entity);
            }
        }


        public virtual void Delete(TPK id)
        {
            var entity = GetById(id);
            Delete(entity);
        }

        public virtual async Task DeleteAsync(TPK id)
        {
            var entity = await GetByIdAsync(id);
            Delete(entity);
        }

        protected virtual bool ValidateDbExists(T entity, bool notExsistsThrowException = false)
        {
            if (entity == null)
            {
                if (notExsistsThrowException) throw new DbUpdateException($"Value of {typeof(T).Name} can't be null ");
                else return false;
            }

            var result = GetById(entity.Id);
            if (notExsistsThrowException == true && result == null)
            {
                throw new DbUpdateException($"Value of {typeof(T).Name} at id = {entity.Id} can't be null ");
            }
            return result != null;
        }

        protected virtual async Task<bool> ValidateDbExistsAsync(T entity, bool notExsistsThrowException = false)
        {
            if (entity == null)
            {
                if (notExsistsThrowException) throw new DbUpdateException($"Value of {typeof(T).Name} can't be null ");
                else return false;
            }

            var result = await GetByIdAsync(entity.Id);
            if (notExsistsThrowException == true && result == null)
            {
                throw new DbUpdateException($"Value of {typeof(T).Name} at id = {entity.Id} can't be null ");
            }
            return result != null;
        }
    }
}
