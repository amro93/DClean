using System;

namespace DClean.Domain.Interfaces
{
    public interface ICreateAuditedEntity<TUserPK> : IEntity
        where TUserPK : struct
    {
        public TUserPK? CreatedById { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
