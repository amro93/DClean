using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Persistence.Models.Location
{
    public class Country : ISoftDeleteAuditedEntity, IMayHaveTenant<Guid?>, IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
        public Guid? TenantId { get; set; }
        public string DeletedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
