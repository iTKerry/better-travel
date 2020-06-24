using System;
using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.EF.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class HotTourConfiguration : IEntityTypeConfiguration<HotTour>
    {
        public void Configure(EntityTypeBuilder<HotTour> builder)
        {
            builder.ToTable(Tables.HotTour, Schemas.Dbo);

            builder.OwnsOne(p => p.Info, p =>
            {
                p.Property(pp => pp.Name).HasColumnName("Name");
                p.Property(pp => pp.Stars)
                    .HasConversion(new EnumToNumberConverter<Stars, int>())
                    .HasColumnName("StarsCount");
                p.Property(pp => pp.DetailsUri)
                    .HasConversion(pp => pp.ToString(), str => new Uri(str))
                    .HasColumnName("DetailsLink");
                p.Property(pp => pp.ImageUri)
                    .HasConversion(pp => pp.ToString(), str => new Uri(str))
                    .HasColumnName("ImageLink");
            });
            
            builder.OwnsOne(p => p.Country, p =>
            {
                p.Property(pp => pp.Name).HasColumnName("CountryName");
                p.Property(pp => pp.DetailsUri)
                    .HasConversion(pp => pp.ToString(), str => new Uri(str))
                    .HasColumnName("CountryDetailsLink");
            });
            
            builder.OwnsOne(p => p.Resort, p =>
            {
                p.Property(pp => pp.Name).HasColumnName("ResortName");
                p.Property(pp => pp.DetailsUri)
                    .HasConversion(pp => pp.ToString(), str => new Uri(str))
                    .HasColumnName("ResortDetailsLink");
            });

            builder.OwnsOne(p => p.Departure, p =>
            {
                p.Property(pp => pp.Location).HasColumnName("DepartureLocation");
                p.Property(pp => pp.Date).HasColumnName("DepartureDate");
            });
            
            builder.OwnsOne(p => p.Duration, p =>
            {
                p.Property(pp => pp.Count).HasColumnName("DurationCount");
                p.Property(pp => pp.Type).HasColumnName("DurationType");
            });

            builder.OwnsOne(p => p.Price, p =>
            {
                p.Property(pp => pp.Amount).HasColumnName("PriceAmount");
                p.Property(pp => pp.Type).HasColumnName("PriceType");
            });
        }
    }
}