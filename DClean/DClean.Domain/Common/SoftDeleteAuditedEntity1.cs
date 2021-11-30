using System;
using DClean.Domain.Interfaces;

namespace DClean.Domain.Common.BaseEntities
{
    public abstract class SoftDeleteAuditedEntity<TUserPK, TUser> : UpdateAuditedEntity<TUserPK, TUser>, ISoftDeleteAuditedEntity<TUserPK, TUser>
        where TUserPK : struct
        where TUser : class
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public TUserPK? DeletedById { get; set; }
        public TUser DeletedBy { get; set; }
    }

}
