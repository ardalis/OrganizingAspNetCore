using Microsoft.EntityFrameworkCore;
using WithFeatureFolders.Core.Model;

namespace WithFeatureFolders.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Ninja> Ninjas { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Pirate> Pirates { get; set; }
        public DbSet<Zombie> Zombies { get; set; }
    }
}
