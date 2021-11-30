using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.Interfaces.Repositories;
using DClean.Domain.Interfaces;
using DClean.Infrastructure.Common.Repositories;
using DClean.Infrastructure.Persistence.Contexts;

namespace DClean.Infrastructure.Persistence.Repositories
{
    public interface IRepository<T> : IBaseRepository<T>
        where T : class, IEntity
    {

    }
    public interface IRepository<T, TPK> : IBaseRepository<T, TPK>
        where T: class, IEntity<TPK>
        where TPK : IEquatable<TPK>
    {

    }
    public class Repository<T, TPK> : BaseRepository<T, TPK>, IRepository<T, TPK>
        where T : class, IEntity<TPK>
        where TPK : IEquatable<TPK>
    {
        public Repository(IDCleanDbContextFactory dbContextFactory, IServiceProvider serviceProvider) : base(dbContextFactory, serviceProvider)
        {
        }
    }

    public class Repository<T> : BaseRepository<T>, IRepository<T>
        where T : class, IEntity
    {   
        public Repository(IDCleanDbContextFactory dbContextFactory, IServiceProvider serviceProvider) : base(dbContextFactory, serviceProvider)
        {
        }
    }
}
