using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Identity;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.BaseEntities.Identity
{
    public class BaseUser : IdentityUser<Guid>, IFullAuditedEntity<Guid?>, IEntity<Guid>, IMayHaveTenant
    {
        public BaseUser()
        {
        }

        public BaseUser(string userName) : base(userName)
        {
        }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DeletedById { get; set; }
        public Guid? TenantId { get; set; }
    }

    public class BaseUser<TUserRole, TUserClaim, TUserToken, TUserLogin> : BaseUser, IEntity<Guid>
    {
        public BaseUser() : base()
        {
        }

        public BaseUser(string userName) : base(userName)
        {
        }

        public virtual ICollection<TUserRole> UserRoles { get; set; }
        public virtual ICollection<TUserClaim> UserClaims { get; set; }
        public virtual ICollection<TUserToken> UserTokens { get; set; }
        public virtual ICollection<TUserLogin> UserLogins { get; set; }
    }
}
