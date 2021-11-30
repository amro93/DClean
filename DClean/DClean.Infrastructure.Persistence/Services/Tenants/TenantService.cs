using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.DTOs.Tenants;
using DClean.Application.Exceptions;
using DClean.Application.Filters;
using DClean.Application.Interfaces.Services;
using DClean.Application.Wrappers;
using DClean.Domain.Entities.Presistence.Identity;
using DClean.Infrastructure.Persistence.Repositories;

namespace DClean.Infrastructure.Persistence.Services.Tenants
{
    public class TenantService : ITenantService
    {
        private readonly IRepository<Tenant, Guid> _tenantRepo;

        public TenantService(
            IRepository<Tenant, Guid> tenantRepo)
        {
            _tenantRepo = tenantRepo;
        }
        public async Task<Guid> CreateAsync(TenantCreateDto dto)
        {
            await CheckDuplicateTenantName(dto.Name);
            var entity = new Tenant()
            {
                Name = dto.Name,
            };
            _tenantRepo.Create(entity);
            await _tenantRepo.SaveAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            _tenantRepo.Delete(id);
            await _tenantRepo.SaveAsync();
        }

        public async Task<TenantDetailsDto> GetAsync(Guid id)
        {
            var query = _tenantRepo.GetTable().Where(t => t.Id == id)
                .Select(t => new TenantDetailsDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    ConcurrencyStamp = t.ConcurrencyStamp,
                    TenantConnectionStrings = t.TenantConnectionStrings.Select(c => new TenantConnectionStringDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ConnectionString = c.ConnectionString,
                        TenantId = c.TenantId,
                    }).ToList()
                });
            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        public Task<TenantDetailsDto> GetByNameAsync(string name)
        {
            return _tenantRepo.GetTable().Select(t => new TenantDetailsDto
            {
                Id = t.Id,
                Name = t.Name,
                ConcurrencyStamp = t.ConcurrencyStamp,
                TenantConnectionStrings = t.TenantConnectionStrings.Select(c => new TenantConnectionStringDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ConnectionString = c.ConnectionString,
                    TenantId = c.TenantId,
                }).ToList()
            }).FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task<Guid?> NameExistsAsync(string name)
        {
            var result = await _tenantRepo.GetTable().Where(t => t.Name == name).FirstOrDefaultAsync();
            return result?.Id;

        }

        public async Task<PagedResponse<List<TenantListDto>>> ListPagedAsync(PagedRequestParameter dto)
        {
            var query = _tenantRepo.GetTable().IgnoreQueryFilters().Where(t => !t.IsDeleted)
                .Select(t => new TenantListDto
                {
                    Id = t.Id,
                    Name = t.Name,

                });
            query = query.Skip(dto.GetSkip()).Take(dto.GetTake());
            var result = await query.ToListAsync();
            return new PagedResponse<List<TenantListDto>>(result, dto.PageNumber, dto.PageSize);
        }

        private async Task CheckDuplicateTenantName(string tenantName)
        {
            var query = _tenantRepo.GetTable().IgnoreQueryFilters().Where(t => t.Name == tenantName);
            var dbTenant = await query.FirstOrDefaultAsync();
            if (dbTenant != null && !dbTenant.IsDeleted) throw new Exception($"Can't set tenant name with value '{tenantName}'");
        }
        public async Task UpdateAsync(TenantUpdateDto dto)
        {
            var dbTenant = await _tenantRepo.GetTable()
                .IgnoreQueryFilters()
                .Where(t => !t.IsDeleted)
                .Where(t => t.Id == dto.Id)
                .FirstOrDefaultAsync();
            if (dbTenant == null) throw new DbUpdateException($"Can't find tenant at id = {dto.Id}");
            dbTenant.IsDisabled = dto.IsDisabled;
            _tenantRepo.Update(dbTenant);
            await _tenantRepo.SaveAsync();
        }

        public async Task ChangeName(TenantChangeNameRequest dto )
        {
            var dbTenant = await _tenantRepo.GetTable()
                   .IgnoreQueryFilters()
                   .Where(t => !t.IsDeleted)
                   .Where(t => t.Id == dto.Id)
                   .FirstOrDefaultAsync();
            if (dbTenant == null) throw new DbUpdateException($"Can't find tenant at id = {dto.Id}");
            if (dto.ConcurrencyStamp != dbTenant.ConcurrencyStamp) throw new Exception("You don't own the latest version of the current object");
            await CheckDuplicateTenantName(dto.Name);
            dbTenant.Name = dto.Name;
            _tenantRepo.Update(dbTenant);
            await _tenantRepo.SaveAsync();
        }
    }
}
