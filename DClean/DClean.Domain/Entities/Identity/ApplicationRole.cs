using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Interfaces;
using DClean.Infrastructure.Common.BaseEntities.Identity;

namespace DClean.Domain.Entities.Presistence.Identity
{
    public class ApplicationRole : BaseRole<ApplicationUserRole, ApplicationRoleClaim>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        
    }
}
