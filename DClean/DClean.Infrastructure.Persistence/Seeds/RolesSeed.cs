using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.Enums;
using DClean.Application.Interfaces;
using DClean.Domain.Entities.Presistence.Identity;

namespace DClean.Infrastructure.Persistence.Seeds
{
    public class RolesSeed : ISeedService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesSeed(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public int Order => 0;

        public async Task SeedAsync()
        {
            //Seed Roles
            if (!await _roleManager.RoleExistsAsync(Roles.SuperAdmin.ToString()))
                await _roleManager.CreateAsync(new ApplicationRole(Roles.SuperAdmin.ToString()) { Id = Guid.Parse("a399ed17-744b-41fe-8c2a-87405ebb7b04"), IsStatic = true });
            if (!await _roleManager.RoleExistsAsync(Roles.Admin.ToString()))
                await _roleManager.CreateAsync(new ApplicationRole(Roles.Admin.ToString()) { Id = Guid.Parse("4d5e8705-b121-4ae4-84d2-3ef85b471b0d"), IsStatic = true });
            if (!await _roleManager.RoleExistsAsync(Roles.Moderator.ToString()))
                await _roleManager.CreateAsync(new ApplicationRole(Roles.Moderator.ToString()) { Id = Guid.Parse("bee28105-c44e-4973-8e6e-da6321939d1c"), IsStatic = true });
            if (!await _roleManager.RoleExistsAsync(Roles.Basic.ToString()))
                await _roleManager.CreateAsync(new ApplicationRole(Roles.Basic.ToString()) { Id = Guid.Parse("a148660c-d27d-40ac-9061-0a27f24a6cdd"), IsStatic = true });
        }
    }
}