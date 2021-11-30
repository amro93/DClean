using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DClean.Application.DTOs.Tenants;
using DClean.Application.Interfaces.Services;

namespace DClean.WebApi.Controllers
{
    public class TenantConnectionStringsController : BaseApiController
    {
        private readonly ITenantConnectionStringService _connectionStringService;

        public TenantConnectionStringsController(
            ITenantConnectionStringService connectionStringService)
        {
            _connectionStringService = connectionStringService;
        }

        [HttpGet]
        public async Task<List<TenantConnectionStringDto>> ListAsync([FromQuery] [Required]Guid tenantId)
        {
            var result = await _connectionStringService.ListAsync(tenantId);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TenantConnectionStringCreateDto dto)
        {
            var result = await _connectionStringService.Create(dto);
            return Ok(new { Id = result });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(TenantConnectionStringUpdateDto dto)
        {
            await _connectionStringService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _connectionStringService.DeleteAsync(id);
            return Ok();
        }
    }
}
