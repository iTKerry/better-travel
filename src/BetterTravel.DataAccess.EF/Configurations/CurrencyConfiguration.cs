using BetterTravel.DataAccess.EF.Metadata;
using BetterTravel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable(Tables.Currency, Schemas.Dbo).HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("CurrencyId").ValueGeneratedNever();
            builder.Property(p => p.Name);
        }
    }
}