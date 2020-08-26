using BetterTravel.DataAccess.EF.Metadata;
using BetterTravel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class HotelCategoryConfiguration: IEntityTypeConfiguration<HotelCategory>
    {
        public void Configure(EntityTypeBuilder<HotelCategory> builder)
        {
            builder.ToTable(Tables.HotelCategory, Schemas.Dbo).HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("HotelCategoryID").ValueGeneratedNever();
            builder.Property(p => p.Name);
        }
    }
}