using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.EF.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class SettingsCountryConfiguration : IEntityTypeConfiguration<SettingsCountry>
    {
        public void Configure(EntityTypeBuilder<SettingsCountry> builder)
        {
            builder.ToTable(Tables.SettingsCountry, Schemas.Dbo).HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("SettingsCountryID");

            builder.HasOne(p => p.Settings).WithMany(p => p.SettingsCountries);
            
            builder.HasOne(p => p.Country).WithMany();
        }
    }
}