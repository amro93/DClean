using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Domain.Entities.Presistence.Identity
{
    public class TenantConnectionString : IFullAuditedEntity<Guid?>, IEntity<Guid>, IMayHaveTenant<Guid?>
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Hashed
        /// </summary>
        public string ConnectionString { get; set; }
        public Guid? DeletedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
