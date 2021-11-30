using Microsoft.AspNetCore.Identity;
using System;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.BaseEntities.Identity
{

    public class BaseUserRole : IdentityUserRole<Guid>, IEntity, IMayHaveTenant
    {
        public BaseUserRole() : base()
        {
        }

        public Guid? TenantId { get; set; }
    }
    public class BaseUserRole<TUser, TRole> : BaseUserRole, IEntity
    {
        public BaseUserRole() : base()
        {
        }

        public virtual TUser User { get; set; }
        public virtual TRole Role { get; set; }
    }
}
