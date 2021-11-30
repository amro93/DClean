using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DClean.Domain.Entities.Presistence.Identity;
using DClean.Infrastructure.Common.EntityMapConfigurationExtensions;
using DClean.Infrastructure.Persistence.Models.Location;

namespace DClean.Infrastructure.Persistence.EntityMapConfiguration.Locations
{
    public class CountryMap : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ConfigureBaseTypes<Country, ApplicationUser>();
            builder.Property(t => t.Name).IsRequired().HasMaxLength(256);
        }
    }

    public class NeighbourhoodMap : IEntityTypeConfiguration<Neighbourhood>
    {
        public void Configure(EntityTypeBuilder<Neighbourhood> builder)
        {
            builder.ConfigureBaseTypes<Neighbourhood, ApplicationUser>();
            builder.Property(t => t.Name).IsRequired().HasMaxLength(256);
            builder.HasOne(t => t.City).WithMany(t => t.Neighbourhoods).HasForeignKey(t => t.CityId);
        }
    }
    public class CityMap : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ConfigureBaseTypes<City, ApplicationUser>();
            builder.Property(t => t.Name).IsRequired().HasMaxLength(256);
            builder.HasOne(t => t.Province).WithMany(t => t.Cities).HasForeignKey(t => t.ProvinceId);
        }
    }
    public class ProvinceMap : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ConfigureBaseTypes<Province, ApplicationUser>();
            builder.Property(t => t.Name).IsRequired().HasMaxLength(256);
            builder.HasOne(t => t.Country).WithMany(t => t.Provinces).HasForeignKey(t => t.CountryId);
        }
    }
}
