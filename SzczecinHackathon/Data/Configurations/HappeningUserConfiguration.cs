using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SzczecinHackathon.Models;

namespace SzczecinHackathon.Data.Configurations
{
    public class HappeningUserConfiguration : IEntityTypeConfiguration<HappeningUser>
    {
        public void Configure(EntityTypeBuilder<HappeningUser> builder)
        {
            builder.HasKey(hu => new { hu.HappeningId, hu.UserId });

            builder.HasOne(hu => hu.User)
                .WithMany(h => h.HappeningUsers)
                .HasForeignKey(hu => hu.HappeningId);

            builder.HasOne(hu => hu.User)
                .WithMany(h => h.HappeningUsers)
                .HasForeignKey(hu => hu.UserId);
        }
    }
}
