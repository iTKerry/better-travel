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
            builder.ToTable(Tables.Chat, Schemas.Dbo).HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("ChatID");
            builder.Property(p => p.ChatId).HasColumnName("TelegramChatID");
            
            builder
                .HasOne(p => p.Settings)
                .WithOne(p => p.Chat)
                .HasForeignKey<ChatSettings>(p => p.SettingsOfChatId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.OwnsOne(p => p.Info, p =>
            {
                p.Property(pp => pp.Title).HasColumnName("Title");
                p.Property(pp => pp.Description).HasColumnName("Description");
                p.Property(pp => pp.Type).HasColumnName("Type");
            });
        }
    }
}