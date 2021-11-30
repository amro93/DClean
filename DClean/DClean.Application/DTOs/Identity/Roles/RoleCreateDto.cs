using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DClean.Application.DTOs.Identity.Roles
{
    public class RoleCreateDto
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsPublic { get; set; }
    }
}
