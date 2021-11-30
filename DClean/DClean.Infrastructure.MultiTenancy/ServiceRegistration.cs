using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VTower.Application.Interfaces;
using VTower.Infrastructure.Persistence.Contexts;
using VTower.Infrastructure.Persistence.Repository;

namespace VTower.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddMultiTenancyInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<TenantDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                Action<DbContextOptionsBuilder> optionsAction = options =>
                {
                    options.UseSqlServer(
                        b => b.MigrationsAssembly(typeof(TenantDbContext).Assembly.FullName));
                    options.UseTriggers(triggerOptions => triggerOptions.AddAssemblyTriggers());
                };
                services.AddTriggeredDbContext<TenantDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("TenantConnection"),
                   b => b.MigrationsAssembly(typeof(TenantDbContext).Assembly.FullName)));
            }
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            //services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            #endregion
        }
    }
}
