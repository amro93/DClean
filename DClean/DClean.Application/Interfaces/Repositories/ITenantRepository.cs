using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Entities.Presistence.Identity;

namespace DClean.Application.Interfaces.Repositories
{
    public interface ITenantRepository : IBaseRepository<Tenant, Guid>
    {
    }
}
