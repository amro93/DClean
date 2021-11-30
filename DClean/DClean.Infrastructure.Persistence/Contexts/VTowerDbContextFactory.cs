using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.Interfaces.Identity;

namespace DClean.Infrastructure.Persistence.Contexts
{
    public class DCleanDbContextFactory : IDCleanDbContextFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DCleanDbContextFactory(
            IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
        }
        public DCleanDbContext CreateDbContext()
        {
            var currentTenant = _serviceProvider.GetRequiredService<ICurrentTenant>();
            var dbContext = _serviceProvider.GetRequiredService<DCleanDbContext>();
            var connectionString = currentTenant.GetConnectionStringAsync().Result;
            dbContext.Database.SetConnectionString(connectionString);
            return dbContext;
        }
    }

    public interface IDCleanDbContextFactory : IDbContextFactory<DCleanDbContext>
    {

    }
}
