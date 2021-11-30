using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.DTOs.DemoRequests;
using DClean.Application.DTOs.StaticFiles;
using DClean.Application.Exceptions;
using DClean.Application.Filters;
using DClean.Application.Interfaces.Services;
using DClean.Application.Wrappers;
using DClean.Infrastructure.Persistence.Onboarding.Models;
using DClean.Infrastructure.Persistence.Repositories;

namespace DClean.Infrastructure.Persistence.Services.Onboarding
{
    public class DemoRequestService : IDemoRequestService
    {
        private readonly IRepository<DemoRequest, Guid> _demoRequestRepo;
        private readonly IStaticFileHelper _staticFileHelper;

        public DemoRequestService(
            IRepository<DemoRequest, Guid> demoRequestRepo,
            IStaticFileHelper staticFileHelper)
        {
            _demoRequestRepo = demoRequestRepo;
            _staticFileHelper = staticFileHelper;
        }
        public async Task<Guid> CreateAsync(DemoRequestCreateDto dto)
        {

            var fileDto = await _staticFileHelper.SaveFileAsync(dto.CompanyLogo);
            var entity = new DemoRequest
            {
                Name = dto.Name,
                MobileNumber = dto.MobileNumber,
                Email = dto.Email,
                CompanyName = dto.CompanyName,
                CompanyNumber = dto.CompanyNumber,
                SaaClientDefault = dto.SaaClientDefault,
                CompanyCountryId = dto.CompanyCountryId,
                CompanyLogo = fileDto?.GetFileInfo(),
                State = EDemoRequestState.New,
            };

            _demoRequestRepo.Create(entity);
            await _demoRequestRepo.SaveAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            _demoRequestRepo.Delete(id);
            await _demoRequestRepo.SaveAsync();
        }

        public async Task<PagedResponse<List<DemoRequestListDto>>> ListPagedAsync(PagedRequestParameter dto)
        {
            var query = _demoRequestRepo.GetTable()
                .Select(t => new DemoRequestListDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Email = t.Email,
                    MobileNumber = t.MobileNumber,
                    SaaClientDefault = t.SaaClientDefault,
                    CompanyName = t.CompanyName,
                    CompanyNumber = t.CompanyNumber,
                    CompanyCountryId = t.CompanyCountryId,
                    CompanyCountryName = t.CompanyCountry.Name,
                    CompanyLogo = new FileDto(t.CompanyLogo).GetTempFilePath(),
                    
                });
            query = query.Skip(dto.GetSkip()).Take(dto.GetTake());

            var result = await query.ToListAsync();
            return new PagedResponse<List<DemoRequestListDto>>(result, dto.PageNumber, dto.PageSize);
        }

        public Task UpdateAsync(DemoRequestUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task SetDemoRequestState(Guid id, EDemoRequestState state)
        {
            var dbRequest = await _demoRequestRepo.GetByIdAsync(id);
            if (dbRequest == null) throw new ApiException("Not Found");

            dbRequest.State = state;
            switch (state)
            {
                case EDemoRequestState.Approved:
                    await ApproveDemoRequest();
                    break;
                case EDemoRequestState.OnHold:
                    break;
                case EDemoRequestState.Rejected:
                    await RejectDemoRequest();
                    break;
                default:
                    break;
            }
            _demoRequestRepo.Update(dbRequest);
            await _demoRequestRepo.SaveAsync();
        }

        private async Task ApproveDemoRequest()
        {

        }

        private async Task RejectDemoRequest()
        {

        }

        public async Task<DemoRequestDetailsDto> GetByIdAsync(Guid id)
        {
            var query = _demoRequestRepo.GetTable()
                .Select(t => new DemoRequestDetailsDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Email = t.Email,
                    MobileNumber = t.MobileNumber,
                    SaaClientDefault = t.SaaClientDefault,
                    CompanyName = t.CompanyName,
                    CompanyNumber = t.CompanyNumber,
                    CompanyCountryId = t.CompanyCountryId,
                    CompanyCountryName = t.CompanyCountry.Name,
                    CompanyLogo = new FileDto(t.CompanyLogo).GetTempFilePath(),

                });

            var result = await query.FirstOrDefaultAsync(t => t.Id == id);
            return result;
        }
    }
}
