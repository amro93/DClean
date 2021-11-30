using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Entities.Presistence.Identity;
using DClean.Infrastructure.Common.EntityMapConfigurationExtensions;
using DClean.Infrastructure.Common.Extensions;

namespace DClean.Infrastructure.Persistence.EntityMapConfiguration.Tenants
{
    public class TenantMap : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ConfigureBaseTypes<Tenant, ApplicationUser>();
            builder.AppendQueryFilter<Tenant>(t => !t.IsDisabled);
        }
    }
}
