using System;
using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.EF.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class HotTourConfiguration : IEntityTypeConfiguration<HotTour>
    {
        public void Configure(EntityTypeBuilder<HotTour> builder)
        {
            builder.ToTable(Tables.HotTour, Schemas.Dbo);
            
            builder.OwnsOne(p => p.Country, p =>
            {
                p.Property<int>("CountryHotTourID").HasColumnName("CountryHotTourID");
                p.Property(pp => pp.Name).HasColumnName("CountryName");
                p.Property(pp => pp.Details).HasColumnName("CountryDetails");
            });
            
            builder.OwnsOne(p => p.Resort, p =>
            {
                p.Property<int>("ResortHotTourID").HasColumnName("ResortHotTourID");
                p.Property(pp => pp.Name).HasColumnName("ResortName");
                p.Property(pp => pp.Details).HasColumnName("ResortDetails");
            });
            
            throw new NotImplementedException();
        }
    }
}