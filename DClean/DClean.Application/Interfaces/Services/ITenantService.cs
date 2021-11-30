using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.DTOs.Tenants;
using DClean.Application.Filters;
using DClean.Application.Wrappers;

namespace DClean.Application.Interfaces.Services
{
    public interface ITenantService
    {
        public Task<Guid> CreateAsync(TenantCreateDto dto);
        public Task<TenantDetailsDto> GetAsync(Guid id);
        public Task<TenantDetailsDto> GetByNameAsync(string name);
        public Task UpdateAsync(TenantUpdateDto dto);
        public Task<PagedResponse<List<TenantListDto>>> ListPagedAsync(PagedRequestParameter dto);
        public Task DeleteAsync(Guid id);
        Task<Guid?> NameExistsAsync(string name);
        Task ChangeName(TenantChangeNameRequest dto);
    }
}
