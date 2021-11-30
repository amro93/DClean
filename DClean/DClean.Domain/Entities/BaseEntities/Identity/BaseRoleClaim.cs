using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.BaseEntities.Identity
{
    public class BaseRoleClaim : IdentityRoleClaim<Guid>, IEntity, IMayHaveTenant
    {
        public BaseRoleClaim()
        {
        }

        public Guid? TenantId { get; set; }
    }

    public class BaseRoleClaim<TRole> : BaseRoleClaim, IEntity
    {
        public BaseRoleClaim()
        {
        }

        public virtual TRole Role { get; set; }
    }
}
