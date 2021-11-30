using System;

namespace DClean.Domain.Interfaces
{
    public interface ISoftDeleteAuditedEntity<TUserPK> : IUpdateAuditedEntity<TUserPK>, ISoftDeleteEntity
        where TUserPK : struct
    {
#nullable enable
        public TUserPK? DeletedById { get; set; }
    }
}
