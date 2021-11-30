using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTower.Domain.Interfaces
{
    public interface ITenantEntity: ITenantEntity<Guid, Guid>
    {

    }
    public interface ITenantEntity<TTenantPK> : IEntity
        where TTenantPK : struct
    {
        public TTenantPK? TenantId { get; set; }
    }

    public interface ITenantEntity<TPK, TTenantPK> : IEntity<TPK>
        where TTenantPK : struct

    {
        public TTenantPK? TenantId { get; set; }
    }
}
