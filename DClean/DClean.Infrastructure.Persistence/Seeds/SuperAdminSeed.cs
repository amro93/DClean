using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.Enums;
using DClean.Application.Interfaces;
using DClean.Domain.Entities.Presistence.Identity;

namespace DClean.Infrastructure.Persistence.Seeds
{
    public class SuperAdminSeed : ISeedService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<SuperAdminSeed> _logger;

        public SuperAdminSeed(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            ILogger<SuperAdminSeed> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public int Order => 1;

        public async Task SeedAsync()
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                Id = Guid.Parse("8c6436f6-9502-4448-97a0-1f24c201d0f5"),
                UserName = "superadmin",
                Email = "amro.samy93@gmail.com",
                FirstName = "Amro",
                LastName = "Sami",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var dbUser = await _userManager.FindByIdAsync(defaultUser.Id.ToString());
            if (dbUser == null)
            {
                var user = await _userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    var userCreationResult = await _userManager.CreateAsync(defaultUser, "P@$$w0rd@123");
                    if (!userCreationResult.Succeeded) _logger.LogCritical("Failed to create super admin {0}", userCreationResult.Errors.ToString());
                    await _userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await _userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    await _userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await _userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }

            }
        }
    }
}
