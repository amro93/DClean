using System;
using System;
namespace DClean.Domain.Interfaces
{
    public interface IFullAuditedEntity<TUserPK, TUser> : IFullAuditedEntity<TUserPK>, ISoftDeleteAuditedEntity<TUserPK, TUser>
        where TUserPK : IEquatable<TUserPK>
        where TUser : class
    {

    }
}
