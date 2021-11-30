using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.DTOs.Identity.Roles;
using DClean.Application.Filters;
using DClean.Application.Interfaces;
using DClean.Application.Wrappers;

namespace DClean.Infrastructure.Persistence.Services.Identity
{
    public class RoleService : IRoleService
    {
        public RoleService()
        {

        }

        public Task<Guid> CreateAsync(RoleCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RoleListDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RoleDetailsDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<RoleListDto>> GetPagedAsync(PagedRequestParameter dto)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateAsync(RoleUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
