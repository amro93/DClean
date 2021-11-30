using DClean.Domain.Interfaces;

namespace DClean.Domain.Common.BaseEntities
{
    public abstract class CreateAuditedEntity<TUserPK, TUser> : CreateAuditedEntity<TUserPK>, ICreateAuditedEntity<TUserPK, TUser>
        where TUserPK : struct
        where TUser : class
    {
        public TUser CreatedBy { get; set; }
    }

}
