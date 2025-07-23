using System;
using DClean.Domain.Interfaces;

namespace DClean.Domain.Common.BaseEntities
{
    public abstract class UpdateAuditedEntity<TUserPK> : CreateAuditedEntity<TUserPK>, IUpdateAuditedEntity<TUserPK>
        where TUserPK : IEquatable<TUserPK>
    {
        public TUserPK? UpdatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public abstract class UpdateAuditedEntity : UpdateAuditedEntity<Guid>, IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
