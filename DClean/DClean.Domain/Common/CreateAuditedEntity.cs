using System;
using DClean.Domain.Interfaces;

namespace DClean.Domain.Common.BaseEntities
{
    public abstract class CreateAuditedEntity<TUserPK> : ICreateAuditedEntity<TUserPK>
        where TUserPK : struct
    {
        public TUserPK? CreatedById { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public abstract class CreateAuditedEntity : CreateAuditedEntity<Guid>, IEntity<Guid>
    {
        public Guid Id { get; set; }
    }

}
