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
    public class BaseUserMap : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.HasIndex(t => t.NormalizedUserName).IsUnique(false);
            builder.HasIndex(t => new { t.NormalizedUserName, t.TenantId }).IsUnique(true);
        }
    }

    public class BaseRoleMap : IEntityTypeConfiguration<BaseRole>
    {
        public void Configure(EntityTypeBuilder<BaseRole> builder)
        {
            builder.HasIndex(t => t.Name).IsUnique(false);
            builder.HasIndex(t => new { t.Name, t.TenantId }).IsUnique();
        }
    }

}
