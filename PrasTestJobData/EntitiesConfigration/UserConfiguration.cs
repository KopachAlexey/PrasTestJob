using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrasTestJobData.Entities;

namespace PrasTestJobData.EntitiesConfigration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id).HasField("_id");
            builder.HasIndex(u => u.Login).IsUnique();
        }
    }
}
