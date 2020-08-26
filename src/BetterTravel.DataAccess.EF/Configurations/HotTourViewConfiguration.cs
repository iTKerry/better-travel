using System;
using BetterTravel.DataAccess.EF.Metadata;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Entities.Enumerations;
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
            builder.Property(p => p.ResortName).HasColumnName("ResortName");
            builder.Property(p => p.DepartureDate).HasColumnName("DepartureDate");
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

            builder
                .Property(p => p.HotelCategory)
                .HasColumnName("HotelCategoryID")
                .HasConversion(p => p.Id, id => HotelCategory.FromId(id));
            
            builder
                .Property(p => p.Country)
                .HasColumnName("CountryID")
                .HasConversion(p => p.Id, id => Country.FromId(id));
            
            builder
                .Property(p => p.DepartureLocation)
                .HasColumnName("DepartureLocationID")
                .HasConversion(p => p.Id, id => DepartureLocation.FromId(id));
        }
    }
}