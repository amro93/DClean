using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DClean.Application.DTOs.Tenants;
using DClean.Application.Filters;
using DClean.Application.Interfaces.Identity;
using DClean.Application.Interfaces.Services;
using DClean.Application.Wrappers;

namespace DClean.WebApi.Controllers
{
    public class TenantsController : BaseApiController
    {
        private readonly ITenantService _tenantService;
        private readonly ICurrentTenant _currentTenant;

        public TenantsController(ITenantService tenantService,
            ICurrentTenant currentTenant)
        {
            _tenantService = tenantService;
            _currentTenant = currentTenant;
        }

        [HttpGet]
        public async Task<PagedResponse<List<TenantListDto>>> ListAsync([FromQuery]PagedRequestParameter dto)
        {
            var result = await _tenantService.ListPagedAsync(dto);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<TenantDetailsDto> GetDetailsAsync([FromRoute] Guid id)
        {
            var result = await _tenantService.GetAsync(id);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<TenantDetailsDto>> GetByName([FromQuery][Required]string tenantName)
        {
            var result = await _tenantService.GetByNameAsync(tenantName);
            if (result == null) return NotFound();
            return result;
        }

        [HttpGet]
        public async Task<Guid?> CheckNameExists([FromQuery][Required] string tenantName)
        {
            var result = await _tenantService.NameExistsAsync(tenantName);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<TenantDetailsDto>> GetCurrentTenant()
        {
            var currentTenantId = _currentTenant.TenantId;
            if (!currentTenantId.HasValue) return BadRequest();
            var result = await _tenantService.GetAsync(currentTenantId.Value);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<TenantDetailsDto>> GetCurrentTenantDetails(bool useTenant)
        {
            if (useTenant)
            {
                _currentTenant.Use(new Guid("87be80c4-1650-4045-bfab-7be922e92349"));
                await Task.Delay(10000);
            }
            var currentTenantId = _currentTenant.TenantId;
            if (!currentTenantId.HasValue) return BadRequest();
            var result = await _tenantService.GetAsync(currentTenantId.Value);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TenantCreateDto dto)
        {
            var result = await _tenantService.CreateAsync(dto);
            return Ok( new { Id = result });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(TenantUpdateDto dto)
        {
            await _tenantService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _tenantService.DeleteAsync(id);
            return Ok();
        }
    }
}
