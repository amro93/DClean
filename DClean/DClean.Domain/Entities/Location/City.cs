using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Persistence.Models.Location
{
    public class City : ISoftDeleteAuditedEntity, IMayHaveTenant<Guid?>, IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Guid ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public virtual ICollection<Neighbourhood> Neighbourhoods { get; set; }
        public Guid? TenantId { get; set; }
        public string DeletedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
