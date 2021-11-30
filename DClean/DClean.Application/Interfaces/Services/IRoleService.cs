using System;
using System.Threading.Tasks;
using DClean.Application.DTOs.Identity.Roles;
using DClean.Application.Filters;
using DClean.Application.Wrappers;

namespace DClean.Application.Interfaces
{
    public interface IRoleService
    {
        Task<RoleListDto> GetAllAsync();
        Task<PagedResponse<RoleListDto>> GetPagedAsync(PagedRequestParameter dto);
        Task<RoleDetailsDto> GetAsync(Guid id);
        Task<Guid> CreateAsync(RoleCreateDto dto);
        Task<Guid> UpdateAsync(RoleUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
