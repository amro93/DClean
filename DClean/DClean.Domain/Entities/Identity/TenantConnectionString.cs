using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Common.BaseEntities;
using DClean.Domain.Interfaces;

namespace DClean.Domain.Entities.Presistence.Identity
{
    public class TenantConnectionString : FullAuditedEntity<Guid>, IEntity<Guid>, IMayHaveTenant<Guid>
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Hashed
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
