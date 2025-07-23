using System;

namespace DClean.Domain.Interfaces
{
    public interface IHaveTenant<TenantPK>
        where TenantPK : IEquatable<TenantPK>
    {
        public TenantPK TenantId { get; set; }
    }
    public interface IHaveTenant : IHaveTenant<Guid>
    {
    }
}
