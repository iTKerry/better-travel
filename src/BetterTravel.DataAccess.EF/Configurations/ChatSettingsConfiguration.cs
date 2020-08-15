using BetterTravel.DataAccess.EF.Metadata;
using BetterTravel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class ChatSettingsConfiguration : IEntityTypeConfiguration<ChatSettings>
    {
        public void Configure(EntityTypeBuilder<ChatSettings> builder)
        {
            builder.ToTable(Tables.ChatSettings).HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("ChatSettingsID");

            builder.HasOne(p => p.Currency);
            
            builder
                .HasMany(p => p.CountrySubscriptions)
                .WithOne(p => p.Settings)
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            builder
                .HasMany(p => p.DepartureSubscriptions)
                .WithOne(p => p.Settings)
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            builder
                .HasOne(p => p.Chat)
                .WithOne(p => p.Settings)
                .HasForeignKey<ChatSettings>(p => p.Id);
        }
    }
}