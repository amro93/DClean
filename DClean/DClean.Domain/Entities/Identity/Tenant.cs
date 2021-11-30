using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Common.BaseEntities;
using DClean.Domain.Interfaces;

namespace DClean.Domain.Entities.Presistence.Identity
{
    public class Tenant : FullAuditedEntity<Guid>, ITrackedEntity, IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ConcurrencyStamp { get; set; }
        public bool IsDisabled { get; set; }
        public ICollection<TenantConnectionString> TenantConnectionStrings { get; set; }
    }
}
