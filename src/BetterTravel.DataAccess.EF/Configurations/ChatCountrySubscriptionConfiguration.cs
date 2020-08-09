using BetterTravel.DataAccess.EF.Metadata;
using BetterTravel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class ChatCountrySubscriptionConfiguration : IEntityTypeConfiguration<ChatCountrySubscription>
    {
        public void Configure(EntityTypeBuilder<ChatCountrySubscription> builder)
        {
            builder.ToTable(Tables.ChatCountrySubscription, Schemas.Dbo).HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("ChatCountrySubscriptionID");
            builder.HasOne(p => p.Settings).WithMany(p => p.CountrySubscriptions);
            builder.HasOne(p => p.Country).WithMany();
        }
    }
}