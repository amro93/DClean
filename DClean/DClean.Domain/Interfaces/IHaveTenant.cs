using System;

namespace DClean.Domain.Interfaces
{
    public interface IHaveTenant<TenantPK>
        where TenantPK : struct
    {
        public TenantPK TenantId { get; set; }
    }
    public interface IHaveTenant : IHaveTenant<Guid>
    {
    }
}
