using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T>
        where T : class, IEntity
    {
        void Create(T entity);
        Task CreateAsync(T entity);
        void CreateMany(IEnumerable<T> entities);
        Task CreateManyAsync(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteMany(IEnumerable<T> entities);
        IQueryable<T> GetTable();
        void PhysicalDelete(T entity);
        void PhysicalDeleteMany(IEnumerable<T> entities);
        int Save();
        Task<int> SaveAsync();
        void Update(T entity);
        void UpdateMany(IEnumerable<T> entities);
    }
    public interface IBaseRepository<T, TPK> : IBaseRepository<T>
        where T : class, IEntity<TPK>
        where TPK : IEquatable<TPK>
    {
        
        void Delete(TPK id);
        Task DeleteAsync(TPK id);
        T GetById(TPK id);
        Task<T> GetByIdAsync(TPK id);
        void PhysicalDelete(TPK id);
        Task PhysicalDeleteAsync(TPK id);
    }
}