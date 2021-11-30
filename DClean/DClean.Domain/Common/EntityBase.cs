using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Common.BaseEntities;
using DClean.Domain.Interfaces;

namespace DClean.Domain.Common.BaseEntities
{
    public abstract class EntityBase : IEntity
    {
    }
    public abstract class Entity<T> : EntityBase, IEntity<T>
    {
        public T Id { get; set; }
    }
}