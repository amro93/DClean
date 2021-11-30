using System;

namespace DClean.Application.DTOs.Identity.Roles
{
    public class RoleUpdateDto : RoleCreateDto
    {
        public Guid Id { get; set; }
    }
}
