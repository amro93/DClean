using System;

namespace DClean.Domain.Interfaces
{
    public interface IUpdateAuditedEntity<TUserPK, TUser> : ICreateAuditedEntity<TUserPK, TUser>, IUpdateAuditedEntity<TUserPK>
        where TUserPK : struct
        where TUser : class
    {
        public TUser UpdatedBy { get; set; }
    }
}
