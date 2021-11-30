using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DClean.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public void SaveChanges();
        public Task SaveChangesAsync();
    }
}
