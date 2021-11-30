using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DClean.Application.DTOs.Identity.UserRoles
{
    public class UserRoleListDto
    {
        public Guid UserRoleId { get; set; }
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsPublic { get; set; }
    }
}
