using System;

namespace DClean.Application.DTOs.Identity.Roles
{
    public class RoleListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsPublic { get; set; }
    }
}
