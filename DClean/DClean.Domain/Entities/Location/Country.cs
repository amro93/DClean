using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Common.BaseEntities;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Persistence.Models.Location
{
    public class Country : SoftDeleteAuditedEntity, IMayHaveTenant<Guid>
    {
        public string Name { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
        public Guid? TenantId { get; set; }
    }
}
