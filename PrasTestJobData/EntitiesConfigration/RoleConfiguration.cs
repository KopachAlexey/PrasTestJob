using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrasTestJobData.Entities;

namespace PrasTestJobData.EntitiesConfigration
{
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.Id).HasField("_id");
            builder.HasIndex(r => r.Name).IsUnique();
        }
    }
}
