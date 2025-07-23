using System;
namespace DClean.Domain.Interfaces
{
    public interface ISoftDeleteAuditedEntity<TUserPK, TUser> : IUpdateAuditedEntity<TUserPK, TUser>, ISoftDeleteEntity, ISoftDeleteAuditedEntity<TUserPK>
        where TUserPK : IEquatable<TUserPK>
        where TUser : class
    {
        public TUser DeletedBy { get; set; }
    }
}
