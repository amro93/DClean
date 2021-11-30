using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Common.BaseEntities;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Persistence.Models.Location
{
    public class Province : SoftDeleteAuditedEntity, IMayHaveTenant<Guid>
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public Guid? TenantId { get; set; }
    }
}
