using System;

namespace DClean.Domain.Interfaces
{
    public interface ICreateAuditedEntity<TUserPK, TUser> : ICreateAuditedEntity<TUserPK>
        where TUserPK : IEquatable<TUserPK>
        where TUser : class
    {
        public TUser CreatedBy { get; set; }
    }
}
