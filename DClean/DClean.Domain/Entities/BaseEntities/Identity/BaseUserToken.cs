using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.BaseEntities.Identity
{
    public class BaseUserToken : IdentityUserToken<Guid>, IEntity, IMayHaveTenant
    {
        public BaseUserToken()
        {
        }

        public Guid? TenantId { get; set; }
    }
    public class BaseUserToken<TUser> : BaseUserToken, IEntity
    {
        public BaseUserToken() : base()
        {
        }
        public virtual TUser User { get; set; }
    }
}
