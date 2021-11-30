using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DClean.Application.DTOs.Tenants
{
    public class TenantCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class TenantUpdateDto
    {
        public Guid Id { get; set; }
        public bool IsDisabled { get; set; }
    }

    public class TenantChangeNameRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ConcurrencyStamp { get; set; }
    }

    public class TenantListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class TenantDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ConcurrencyStamp { get; set; }
        public List<TenantConnectionStringDto> TenantConnectionStrings { get; set; }
    }
}
