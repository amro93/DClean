using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Persistence.Models.Location
{
    public class Neighbourhood : ISoftDeleteAuditedEntity, IMayHaveTenant<Guid?>, IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CityId { get; set; }
        public virtual City City { get; set; }
        public Guid? TenantId { get; set; }
        public string DeletedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
