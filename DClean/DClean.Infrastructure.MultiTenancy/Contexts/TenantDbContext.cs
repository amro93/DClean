using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VTower.Application.Interfaces;
using VTower.Application.Interfaces.Identity;
using VTower.Domain.Common;
using VTower.Domain.Common.BaseEntities;
using VTower.Domain.Interfaces;

namespace VTower.Infrastructure.Persistence.Contexts
{
    public class TenantDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly ICurrentUser _authenticatedUser;

        public TenantDbContext(DbContextOptions<TenantDbContext> options, IDateTimeService dateTime, ICurrentUser authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        //public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            
            base.OnModelCreating(builder);
        }
    }
}
