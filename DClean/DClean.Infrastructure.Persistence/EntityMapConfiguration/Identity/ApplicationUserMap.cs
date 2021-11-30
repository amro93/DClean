using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Entities.Presistence.Identity;
using DClean.Infrastructure.Common.BaseEntities.Identity;

namespace DClean.Infrastructure.Persistence.EntityMapConfiguration.Identity
{
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasIndex(t => t.NormalizedUserName).IsUnique(false);
            builder.HasIndex(t => new { t.NormalizedUserName, t.TenantId }).IsUnique(true);
        }
    }

    public class ApplicationRoleMap : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasIndex(t => t.Name).IsUnique(false);
            builder.HasIndex(t => new { t.Name, t.TenantId }).IsUnique();
        }
    }

}
