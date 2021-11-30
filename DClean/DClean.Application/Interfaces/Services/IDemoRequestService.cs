using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.DTOs.DemoRequests;
using DClean.Application.Filters;
using DClean.Application.Wrappers;

namespace DClean.Application.Interfaces.Services
{
    public interface IDemoRequestService
    {
        Task<Guid> CreateAsync(DemoRequestCreateDto dto);
        Task<PagedResponse<List<DemoRequestListDto>>> ListPagedAsync(PagedRequestParameter dto);
        Task<DemoRequestDetailsDto> GetByIdAsync(Guid id);
        Task UpdateAsync(DemoRequestUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
