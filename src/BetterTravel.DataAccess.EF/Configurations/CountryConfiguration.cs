using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.EF.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable(Tables.Country, Schemas.Dbo).HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("CountryID");
            builder.Property(p => p.Name);
        }
    }
}