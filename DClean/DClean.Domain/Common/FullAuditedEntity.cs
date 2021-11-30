using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Domain.Common.BaseEntities
{
    public abstract class FullAuditedEntity<TUserPK> : SoftDeleteAuditedEntity<TUserPK>, IFullAuditedEntity<TUserPK>
        where TUserPK : struct
    {
    }
    public abstract class FullAuditedEntity<TUserPK, TUser> : SoftDeleteAuditedEntity<TUserPK, TUser>, IFullAuditedEntity<TUserPK, TUser>
        where TUser : class
        where TUserPK : struct
    {

    }
}
