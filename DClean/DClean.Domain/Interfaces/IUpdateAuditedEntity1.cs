using System;

namespace DClean.Domain.Interfaces
{
    public interface IUpdateAuditedEntity<TUserPK, TUser> : ICreateAuditedEntity<TUserPK, TUser>, IUpdateAuditedEntity<TUserPK>
        where TUserPK : IEquatable<TUserPK>
        where TUser : class
    {
        public TUser UpdatedBy { get; set; }
    }
}
