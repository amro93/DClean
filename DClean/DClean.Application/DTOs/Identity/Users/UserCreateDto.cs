using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DClean.Application.DTOs.Identity.Users
{
    public class UserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual string PhoneNumber { get; set; }
        //public virtual string Email { get; set; }
        //public virtual string UserName { get; set; }
    }
}
