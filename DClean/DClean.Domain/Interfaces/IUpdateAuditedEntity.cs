using System;

namespace DClean.Domain.Interfaces
{
    public interface IUpdateAuditedEntity<TUserPK> : ICreateAuditedEntity<TUserPK> 
        where TUserPK : struct
    {
        public TUserPK? UpdatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
