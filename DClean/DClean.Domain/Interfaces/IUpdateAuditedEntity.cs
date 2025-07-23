using System;

namespace DClean.Domain.Interfaces
{
    public interface IUpdateAuditedEntity<TUserPK> 
        where TUserPK : allows ref struct
    {
        public TUserPK? UpdatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public interface IUpdateAuditedEntity : IUpdateAuditedEntity<string>
    {

    }
}
