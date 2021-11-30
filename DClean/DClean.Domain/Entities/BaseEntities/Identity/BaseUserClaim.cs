using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.BaseEntities.Identity
{
    public class BaseUserClaim : IdentityUserClaim<Guid>, IEntity, IMayHaveTenant
    {
        public BaseUserClaim()
        {
        }

        public Guid? TenantId { get; set; }
    }
    public class BaseUserClaim<TUser> : BaseUserClaim, IEntity
    {
        public BaseUserClaim()
        {
        }

        public virtual TUser User { get; set; }
    }
}
