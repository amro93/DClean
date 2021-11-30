using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DClean.Application.Interfaces;
using DClean.Application.Interfaces.Identity;
using DClean.Domain.Entities.Presistence.Identity;
using DClean.Domain.Interfaces;
using DClean.Infrastructure.Common.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DClean.Infrastructure.Persistence.EntityMapConfiguration.Identity;

namespace DClean.Infrastructure.Persistence.Contexts
{
    public class DCleanDbContext : IdentityDbContext<
        ApplicationUser,
        ApplicationRole, Guid,
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaim,
        ApplicationUserToken>
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly ICurrentUser _currentUser;
        public DCleanDbContext(DbContextOptions<DCleanDbContext> options) : base(options)
        {
            _currentTenant = this.GetService<ICurrentTenant>();
            _currentUser = this.GetService<ICurrentUser>();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<ApplicationUserRole>(entity =>
            {
                entity.HasOne(t => t.Role).WithMany(t => t.UserRoles).HasForeignKey(t => t.RoleId);
                entity.HasOne(t => t.User).WithMany(t => t.UserRoles).HasForeignKey(t => t.UserId);
            });

            builder.Entity<ApplicationUserClaim>(entity =>
            {
                entity.HasOne(t => t.User).WithMany(t => t.UserClaims).HasForeignKey(t => t.UserId);
            });

            builder.Entity<ApplicationUserLogin>(entity =>
            {
                entity.HasOne(t => t.User).WithMany(t => t.UserLogins).HasForeignKey(t => t.UserId);
            });

            builder.Entity<ApplicationRoleClaim>(entity =>
            {
                entity.HasOne(t => t.Role).WithMany(t => t.RoleClaims).HasForeignKey(t => t.RoleId);

            });

            builder.Entity<ApplicationUserToken>(entity =>
            {
                entity.HasOne(t => t.User).WithMany(t => t.UserTokens).HasForeignKey(t => t.UserId);
            });
            //builder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly, t => t.Namespace.Contains(".EntityMapConfiguration"));
            var typesToRegister = typeof(DCleanDbContext).Assembly.GetTypes()
                .Where(type => type.IsClass && type.Namespace != null && type.Namespace.Contains("EntityMapConfiguration"))
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)) && type.IsClass);

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }

            var tenantId = _currentTenant?.TenantId;
            builder.SetQueryFilterOnAllEntities<ISoftDeleteEntity>(t => !t.IsDeleted);
            builder.SetQueryFilterOnAllEntities<IHaveTenant>(t => t.TenantId == tenantId);
            builder.SetQueryFilterOnAllEntities<IMayHaveTenant>(t => t.TenantId == _currentTenant.TenantId);
        }
    }
}
