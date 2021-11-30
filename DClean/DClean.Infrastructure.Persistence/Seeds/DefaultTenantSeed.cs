using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.Interfaces;
using DClean.Domain.Entities.Presistence.Identity;
using DClean.Infrastructure.Persistence.Repositories;

namespace DClean.Infrastructure.Persistence.Seeds
{
    public class DefaultTenantSeed : ISeedService
    {
        private readonly IRepository<Tenant, Guid> _tenantRepo;

        public int Order => -1;
        public DefaultTenantSeed(
            IRepository<Tenant, Guid> tenantRepo)
        {
            _tenantRepo = tenantRepo;
        }
        public async Task SeedAsync()
        {
            var tenant = new Tenant()
            {
                Id = Guid.Parse("87be80c4-1650-4045-bfab-7be922e92349"),
                Name = "Default",
                ConcurrencyStamp = Guid.NewGuid(),
            };
            var dbTenant = await _tenantRepo.GetTable().IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id == tenant.Id);
            if (dbTenant != null) return;
            _tenantRepo.Create(tenant);
            await _tenantRepo.SaveAsync();
        }
    }
}
