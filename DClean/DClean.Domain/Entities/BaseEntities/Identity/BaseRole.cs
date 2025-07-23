using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.BaseEntities.Identity
{
    public class BaseRole : IdentityRole<Guid>, IEntity<Guid>, IMayHaveTenant, ISoftDeleteEntity
    {
        public BaseRole()
        {
        }

        public BaseRole(string roleName) : base(roleName)
        {
        }

        public bool IsDefault { get; set; }
        public bool IsPublic { get; set; }
        public bool IsStatic { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? DeletedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class BaseRole<TUserRole, TRoleClaim> : BaseRole, IEntity
    {
        public BaseRole()
        {
        }

        public BaseRole(string roleName) : base(roleName)
        {
        }

        public virtual ICollection<TUserRole> UserRoles { get; set; }
        public virtual ICollection<TRoleClaim> RoleClaims { get; set; }
    }
}
