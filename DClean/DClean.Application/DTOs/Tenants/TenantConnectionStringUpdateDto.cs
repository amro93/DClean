using System;

namespace DClean.Application.DTOs.Tenants
{
    public class TenantConnectionStringUpdateDto : TenantConnectionStringCreateDto
    {
        public Guid Id { get; set; }
    }
}
