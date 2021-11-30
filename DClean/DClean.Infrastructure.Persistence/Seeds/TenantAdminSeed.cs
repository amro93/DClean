using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.Interfaces;
using DClean.Application.Interfaces.Identity;
using DClean.Domain.Entities.Presistence.Identity;
using DClean.Infrastructure.Persistence.Repositories;

namespace DClean.Infrastructure.Persistence.Seeds
{
    public class TenantAdminSeed : ISeedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICurrentTenant _currentTenant;
        private readonly IRepository<ApplicationUser, Guid> _userRepo;
        private readonly IRepository<ApplicationUserRole> _userRoleRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TenantAdminSeed> _logger;

        public TenantAdminSeed(IServiceProvider serviceProvider,
            ICurrentTenant currentTenant,
            IRepository<ApplicationUser, Guid> userRepo,
            IRepository<ApplicationUserRole> userRoleRepo,
            UserManager<ApplicationUser> userManager,
            ILogger<TenantAdminSeed> logger)
        {
            _serviceProvider = serviceProvider;
            _currentTenant = currentTenant;
            _userRepo = userRepo;
            _userRoleRepo = userRoleRepo;
            _userManager = userManager;
            _logger = logger;
        }
        public int Order => 2;

        public async Task SeedAsync()
        {
            using (_currentTenant.Use(new Guid("87be80c4-1650-4045-bfab-7be922e92349")))
            {
                // Defualt tenant id
                var adminUser = new ApplicationUser()
                {
                    Id = new Guid("e2ffd1dd-eab1-4ad6-b9d2-6fd829828baa"),
                    UserName = "superadmin",
                    Email = "amro.samy93@gmail.com",
                    FirstName = "Amro",
                    LastName = "Sami",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                var dbAdminUser = await _userRepo.GetTable().FirstOrDefaultAsync(t => t.Id == adminUser.Id);
                if (dbAdminUser != null) return;
                //var userCreationResult = await userManager.CreateAsync(adminUser, "P@$$w0rd@123");

                _userRepo.Create(adminUser);
                await _userRepo.SaveAsync();
                //if (!userCreationResult.Succeeded) _logger.LogCritical("Failed to create super admin {0}", userCreationResult.Errors.ToString());
            }
        }
    }
}
