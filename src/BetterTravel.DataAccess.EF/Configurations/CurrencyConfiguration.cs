using BetterTravel.DataAccess.EF.Metadata;
using BetterTravel.DataAccess.Entities.Enumerations;
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
            
            builder.Property(p => p.Code).HasColumnName("Code");
            builder.Property(p => p.Sign).HasColumnName("Sign");
        }
    }
}