using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SzczecinHackathon.Models;

namespace SzczecinHackathon.Data.Configurations
{
    public class HappeningConfiguration : IEntityTypeConfiguration<Happening>
    {
        public void Configure(EntityTypeBuilder<Happening> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
