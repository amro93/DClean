using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DClean.Application.DTOs.DemoRequests;
using DClean.Application.Filters;
using DClean.Application.Interfaces.Services;
using DClean.Application.Wrappers;

namespace DClean.WebApi.Controllers
{
    public class DemoRequestsController : BaseApiController
    {
        public DemoRequestsController(
            IDemoRequestService demoRequestService)
        {
            _demoRequestService = demoRequestService;
        }

        private readonly IDemoRequestService _demoRequestService;

        [HttpGet]
        public async Task<PagedResponse<List<DemoRequestListDto>>> ListAsync([FromQuery] PagedRequestParameter dto)
        {
            var result = await _demoRequestService.ListPagedAsync(dto);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(DemoRequestCreateDto dto)
        {
            var result = await _demoRequestService.CreateAsync(dto);
            return Ok(new { Id = result });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(DemoRequestUpdateDto dto)
        {
            await _demoRequestService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _demoRequestService.DeleteAsync(id);
            return Ok();
        }
    }
}
