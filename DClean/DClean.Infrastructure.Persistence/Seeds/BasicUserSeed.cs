using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class BasicUserSeed : ISeedService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public BasicUserSeed(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public int Order => 2;

        public async Task SeedAsync()
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                Id = Guid.Parse("258a2ddc-acea-47d1-b49a-895dec6f5730"),
                UserName = "basicuser",
                Email = "basic.user@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var dbUser = await _userManager.Users.AnyAsync(u => u.Id != defaultUser.Id);
            if (dbUser)
            {
                var user = await _userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await _userManager.CreateAsync(defaultUser, "P@$$w0rd@123");
                    await _userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }
    }
}
