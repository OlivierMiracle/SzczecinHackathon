using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SzczecinHackathon.Models;

namespace SzczecinHackathon.Data.Configurations
{
    public class ChatUserConfiguration : IEntityTypeConfiguration<ChatUser>
    {
        public void Configure(EntityTypeBuilder<ChatUser> builder)
        {
            builder.HasKey(hu => new { hu.ChatId, hu.UserId });

            builder.HasOne(hu => hu.Chat)
                .WithMany(h => h.ChatUsers)
                .HasForeignKey(hu => hu.ChatId);

            builder.HasOne(hu => hu.User)
                .WithMany(h => h.ChatUsers)
                .HasForeignKey(hu => hu.UserId);
        }
    }
}
