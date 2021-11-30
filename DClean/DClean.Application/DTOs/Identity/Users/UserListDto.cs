using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DClean.Application.DTOs.Identity.Users
{
    public class UserListDto
    {
        public virtual Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual DateTimeOffset? LockoutEnd { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual string UserName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedById { get; set; }
        public Guid? TenantId { get; set; }
    }
}
