using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.EF.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class ChatDepartureSubscriptionConfiguration : IEntityTypeConfiguration<ChatDepartureSubscription>
    {
        public void Configure(EntityTypeBuilder<ChatDepartureSubscription> builder)
        {
            builder.ToTable(Tables.ChatDepartureSubscription, Schemas.Dbo).HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("ChatDepartureSubscriptionID");
            builder.HasOne(p => p.Settings).WithMany(p => p.DepartureSubscriptions);
            builder.HasOne(p => p.Departure).WithMany();
        }
    }
}