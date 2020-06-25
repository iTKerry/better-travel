using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.EF.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class DepartureLocationConfiguration : IEntityTypeConfiguration<DepartureLocation>
    {
        public void Configure(EntityTypeBuilder<DepartureLocation> builder)
        {
            builder.ToTable(Tables.DepartureLocation, Schemas.Dbo).HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("DepartureLocationID");
            builder.Property(p => p.Name);
        }
    }
}