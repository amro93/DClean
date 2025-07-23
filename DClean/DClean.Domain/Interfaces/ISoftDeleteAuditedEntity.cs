using System;

namespace DClean.Domain.Interfaces
{
    public interface ISoftDeleteAuditedEntity<TUserPK> : ISoftDeleteEntity
        where TUserPK : allows ref struct
    {
        public TUserPK? DeletedById { get; set; }
    }
    public interface ISoftDeleteAuditedEntity : ISoftDeleteAuditedEntity<string>
    {

    }
}
