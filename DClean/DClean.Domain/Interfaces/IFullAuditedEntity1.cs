namespace DClean.Domain.Interfaces
{
    public interface IFullAuditedEntity<TUserPK, TUser> : IFullAuditedEntity<TUserPK>, ISoftDeleteAuditedEntity<TUserPK, TUser>
        where TUserPK : struct
        where TUser : class
    {

    }
}
