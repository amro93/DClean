using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Domain.Entities.Presistence.Identity
{
    public class OrganizationUnitRole : IEntity<Guid>, IMayHaveTenant
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
    }
}
