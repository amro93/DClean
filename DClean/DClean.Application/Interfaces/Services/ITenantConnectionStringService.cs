using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DClean.Application.DTOs.Tenants;
using DClean.Application.Filters;
using DClean.Application.Wrappers;

namespace DClean.Application.Interfaces.Services
{
    public interface ITenantConnectionStringService
    {
        public Task<string> GetByTenantIdAsync(Guid tenantId, string name = "DefaultConnection", CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id);
        public Task<List<TenantConnectionStringDto>> ListAsync(Guid tenantId);
        public Task<Guid> Create(TenantConnectionStringCreateDto dto, CancellationToken cancellationToken = default);
        public Task UpdateAsync(TenantConnectionStringUpdateDto dto, CancellationToken cancellationToken = default);
    }
}
