using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Entities.Presistence.Identity;
using DClean.Infrastructure.Common.EntityMapConfigurationExtensions;
using DClean.Infrastructure.Common.SharedEntities;

namespace DClean.Infrastructure.Persistence.EntityMapConfiguration
{
    public class StaticFileInfoMap : IEntityTypeConfiguration<StaticFileInfo>
    {
        public void Configure(EntityTypeBuilder<StaticFileInfo> builder)
        {
            builder.ConfigureBaseTypes<StaticFileInfo, ApplicationUser>();
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.FileName).IsRequired().HasMaxLength(512);
            builder.Property(t => t.Extension).IsRequired(false).HasMaxLength(16);
            builder.Property(t => t.FolderPath).IsRequired(false);
            builder.Property(t => t.StaticFileProvider).HasConversion<string>();
        }
    }
}
