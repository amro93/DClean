using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DClean.Application.DTOs.Tenants;
using DClean.Application.Filters;
using DClean.Application.Interfaces.Services;
using DClean.Application.Wrappers;
using DClean.Domain.Entities.Presistence.Identity;
using DClean.Infrastructure.Persistence.Repositories;

namespace DClean.Infrastructure.Persistence.Services.Tenants
{
    public class TenantConnectionStringService : ITenantConnectionStringService
    {
        private readonly IRepository<TenantConnectionString, Guid> _tenantConnectionStrRepo;

        public TenantConnectionStringService(
            IRepository<TenantConnectionString, Guid> tenantConnectionStrRepo)
        {
            _tenantConnectionStrRepo = tenantConnectionStrRepo;
        }
        public async Task<Guid> Create(TenantConnectionStringCreateDto dto, CancellationToken cancellationToken = default)
        {
            var entity = new TenantConnectionString()
            {
                Name = dto.Name,
                TenantId = dto.TenantId,
                ConnectionString = dto.ConnectionString,
            };
            _tenantConnectionStrRepo.Create(entity);
            await _tenantConnectionStrRepo.SaveAsync();
            return entity.Id;
        }

        public async Task<List<TenantConnectionStringDto>> ListByTenantIdAsync(Guid TenantId)
        {
            var query = _tenantConnectionStrRepo.GetTable().Where(t => t.TenantId == TenantId)
                .Select(t =>new TenantConnectionStringDto
                {
                    Id = t.Id,
                    TenantId = t.TenantId,
                    Name = t.Name,
                    ConnectionString = t.ConnectionString,
                });
            var results = await query.ToListAsync();
            return results;
        }
        public async Task<string> GetByTenantIdAsync(Guid tenantId, string name = "Default", CancellationToken cancellationToken = default)
        {
            var connStr = await _tenantConnectionStrRepo.GetTable()
                .Where(t => t.Name == name && t.TenantId == tenantId)
                .Select(t => t.ConnectionString)
                .FirstOrDefaultAsync(cancellationToken);
            return connStr;
        }

        public async Task UpdateAsync(TenantConnectionStringUpdateDto dto, CancellationToken cancellationToken = default)
        {
            var entity = new TenantConnectionString()
            {
                Id = dto.Id,
                TenantId = dto.TenantId,
                ConnectionString = dto.ConnectionString,
                Name = dto.Name
            };

            _tenantConnectionStrRepo.Update(entity);
            await _tenantConnectionStrRepo.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            _tenantConnectionStrRepo.Delete(id);
            await _tenantConnectionStrRepo.SaveAsync();
        }

        public async Task<List<TenantConnectionStringDto>> ListAsync(Guid tenantId)
        {
            var query = _tenantConnectionStrRepo.GetTable().Where(t => t.TenantId == tenantId)
                .Select(t => new TenantConnectionStringDto
                {
                    Id = t.Id,
                    TenantId = t.TenantId,
                    Name = t.Name,
                    ConnectionString = t.ConnectionString
                });
            var result = await query.ToListAsync();
            return result;
        }
    }
}
