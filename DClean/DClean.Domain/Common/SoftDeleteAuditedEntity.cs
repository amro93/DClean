using System;
using DClean.Domain.Interfaces;

namespace DClean.Domain.Common.BaseEntities
{
    public abstract class SoftDeleteAuditedEntity<TUserPK> : UpdateAuditedEntity<TUserPK>, ISoftDeleteAuditedEntity<TUserPK>
        where TUserPK : IEquatable<TUserPK>
    {
        public TUserPK? DeletedById { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }

    public abstract class SoftDeleteAuditedEntity : SoftDeleteAuditedEntity<Guid>, IEntity<Guid>
    {
        public Guid Id { get; set; }
    }

}
