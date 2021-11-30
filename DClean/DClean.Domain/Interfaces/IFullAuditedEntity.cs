namespace DClean.Domain.Interfaces
{
    public interface IFullAuditedEntity<TUserPK> : ITrackedEntity, ISoftDeleteAuditedEntity<TUserPK>
        where TUserPK : struct
    {

    }
}
