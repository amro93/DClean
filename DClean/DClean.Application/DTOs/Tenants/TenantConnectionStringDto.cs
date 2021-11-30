using System;

namespace DClean.Application.DTOs.Tenants
{
    public class TenantConnectionStringDto
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}
