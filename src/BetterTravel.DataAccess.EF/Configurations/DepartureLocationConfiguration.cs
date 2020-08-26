using BetterTravel.DataAccess.EF.Metadata;
using BetterTravel.DataAccess.Entities.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class DepartureLocationConfiguration : IEntityTypeConfiguration<DepartureLocation>
    {
        public void Configure(EntityTypeBuilder<DepartureLocation> builder)
        {
            builder.ToTable(Tables.DepartureLocation, Schemas.Dbo).HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("DepartureLocationID").ValueGeneratedNever();
            builder.Property(p => p.Name);
        }
    }
}