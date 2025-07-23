using DClean.Domain.Interfaces;
using System;

namespace DClean.Domain.Common.BaseEntities
{
    public abstract class CreateAuditedEntity<TUserPK, TUser> : CreateAuditedEntity<TUserPK>, ICreateAuditedEntity<TUserPK, TUser>
        where TUserPK : allows ref struct
        where TUser : class
    {
        public TUser CreatedBy { get; set; }
    }

}
