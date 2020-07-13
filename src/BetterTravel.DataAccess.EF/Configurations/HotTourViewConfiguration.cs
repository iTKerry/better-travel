using System;
using BetterTravel.DataAccess.EF.Metadata;
using BetterTravel.DataAccess.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class HotTourViewConfiguration : IEntityTypeConfiguration<HotTourView>
    {
        public void Configure(EntityTypeBuilder<HotTourView> builder)
        {
            builder.ToView(Metadata.Views.HotTourView, Schemas.Dbo).HasNoKey();

            builder.Property(p => p.Name).HasColumnName("Name");
            builder.Property(p => p.CountryName).HasColumnName("CountryName");
            builder.Property(p => p.ResortName).HasColumnName("ResortName");
            builder.Property(p => p.HotelCategory).HasColumnName("HotelCategory");
            builder.Property(p => p.DepartureDate).HasColumnName("DepartureDate");
            builder.Property(p => p.DepartureName).HasColumnName("DepartureName");
            builder.Property(p => p.DurationCount).HasColumnName("DurationCount");
            builder.Property(p => p.DurationType).HasColumnName("DurationType");
            builder.Property(p => p.PriceAmount).HasColumnName("PriceAmount");
            builder.Property(p => p.PriceType).HasColumnName("PriceType");
            
            builder
                .Property(p => p.ImageLink)
                .HasConversion(p => p.ToString(), str => new Uri(str));
            builder
                .Property(p => p.DetailsLink)
                .HasConversion(p => p.ToString(), str => new Uri(str));
        }
    }
}