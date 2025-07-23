using System;
using DClean.Domain.Interfaces;

namespace DClean.Domain.Common.BaseEntities
{
    public abstract class UpdateAuditedEntity<TUserPK, TUser> : CreateAuditedEntity<TUserPK, TUser>, IUpdateAuditedEntity<TUserPK, TUser>
        where TUserPK : IEquatable<TUserPK>
        where TUser : class
    {
        public TUser UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TUserPK? UpdatedById { get; set; }
    }

}
