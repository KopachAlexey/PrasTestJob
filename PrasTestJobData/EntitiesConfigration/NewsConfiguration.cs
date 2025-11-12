using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrasTestJobData.Entities;

namespace PrasTestJobData.EntitiesConfigration
{
    class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.Property(n => n.Id).HasField("_id");
        }
    }
}
