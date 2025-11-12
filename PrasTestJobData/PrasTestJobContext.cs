using Microsoft.EntityFrameworkCore;
using PrasTestJobData.Entities;
using PrasTestJobData.EntitiesConfigration;

namespace PrasTestJobData
{
    public class PrasTestJobContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<News> News { get; set; }

        public PrasTestJobContext(DbContextOptions<PrasTestJobContext> options) :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
        }
    }
}
