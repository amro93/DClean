using System;
namespace DClean.Domain.Interfaces
{
    public interface IFullAuditedEntity<TUserPK> : ITrackedEntity, ISoftDeleteAuditedEntity<TUserPK>, ICreateAuditedEntity<TUserPK>, IUpdateAuditedEntity<TUserPK>
        where TUserPK : allows ref struct
    {

    }

    public interface IFullAuditedEntity : IFullAuditedEntity<string?>
    {

    }
}
