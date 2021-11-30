using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Common.BaseEntities;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Persistence.Models.Location
{
    public class Neighbourhood : SoftDeleteAuditedEntity, IMayHaveTenant<Guid>
    {
        public string Name { get; set; }
        public Guid? CityId { get; set; }
        public virtual City City { get; set; }
        public Guid? TenantId { get; set; }
    }
}
