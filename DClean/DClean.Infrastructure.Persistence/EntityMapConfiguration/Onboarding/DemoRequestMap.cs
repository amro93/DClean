using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Entities.Presistence.Identity;
using DClean.Infrastructure.Common.EntityMapConfigurationExtensions;
using DClean.Infrastructure.Persistence.Onboarding.Models;

namespace DClean.Infrastructure.Persistence.EntityMapConfiguration.Onboarding
{
    public class DemoRequestMap : IEntityTypeConfiguration<DemoRequest>
    {
        public void Configure(EntityTypeBuilder<DemoRequest> builder)
        {
            builder.ConfigureBaseTypes<DemoRequest, ApplicationUser>();
            builder.Property(t => t.Name).IsRequired().HasMaxLength(128);
            builder.Property(t => t.MobileNumber).IsRequired().HasMaxLength(128);
            builder.Property(t => t.Email).IsRequired().HasMaxLength(128);
            builder.Property(t => t.CompanyName).IsRequired().HasMaxLength(128);
            builder.Property(t => t.CompanyNumber).IsRequired(false).HasMaxLength(128);
            builder.Property(t => t.SaaClientDefault).IsRequired().HasMaxLength(512);

            builder.HasOne(t => t.CompanyLogo).WithMany().HasForeignKey(t => t.CompanyLogoId);
            builder.HasOne(t => t.CompanyCountry).WithMany().HasForeignKey(t => t.CompanyCountryId);
        }
    }
}
