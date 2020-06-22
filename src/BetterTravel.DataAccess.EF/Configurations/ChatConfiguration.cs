using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.EF.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetterTravel.DataAccess.EF.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable(Tables.Chat, Schemas.Dbo);

            builder
                .Property(p => p.IsSubscribed)
                .HasColumnName("Subscribed");
            
            builder.OwnsOne(p => p.Info, p =>
            {
                p.Property(pp => pp.Title).HasColumnName("Title");
                p.Property(pp => pp.Description).HasColumnName("Description");
                p.Property(pp => pp.Type).HasColumnName("Type");
            });
        }
    }
}