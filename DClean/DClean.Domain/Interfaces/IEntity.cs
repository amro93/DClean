using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DClean.Domain.Interfaces
{
    public interface IEntity
    {
    }

    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}
