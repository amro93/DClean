using System;

namespace DClean.Domain.Interfaces
{
    public interface IMayHaveTenant<TenantPK>
        where TenantPK : allows ref struct
    {
        public TenantPK TenantId { get; set; }
    }
    public interface IMayHaveTenant : IMayHaveTenant<Guid?>
    {
    }
}
