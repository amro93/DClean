using System;

namespace DClean.Domain.Interfaces
{
    public interface ICreateAuditedEntity<TUserPK> : IEntity
        where TUserPK : allows ref struct
    {
        public TUserPK? CreatedById { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public interface ICreateAuditedEntity: ICreateAuditedEntity<string>
    {
        
    }
}
