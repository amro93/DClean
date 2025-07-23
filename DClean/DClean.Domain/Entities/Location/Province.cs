using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Persistence.Models.Location
{
    public class Province : ISoftDeleteAuditedEntity, IMayHaveTenant<Guid?>, IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public Guid? TenantId { get; set; }
        public string DeletedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
