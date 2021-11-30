using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DClean.Application.DTOs.Identity.Roles;
using DClean.Application.Filters;
using DClean.Application.Wrappers;

namespace DClean.WebApi.Controllers
{
    public class RolesController : BaseApiController
    {
        public RolesController()
        {

        }

        [HttpGet]
        public async Task<PagedResponse<List<RoleListDto>>> GetAsync([FromQuery]PagedRequestParameter dto)
        {
            return new PagedResponse<List<RoleListDto>>(new List<RoleListDto>(), dto.PageNumber, dto.PageSize);
        }

        [HttpGet]
        public async Task<List<RoleListDto>> GetAllAsync()
        {
            return new List<RoleListDto>();
        }

        [HttpGet]
        public async Task<RoleDetailsDto> GetDetailsAsync([FromRoute][Required] Guid id)
        {
            return new RoleDetailsDto();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RoleCreateDto dto)
        {
            return Ok(new { Id = Guid.NewGuid() });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] RoleUpdateDto dto)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            return Ok();
        }
    }
}
