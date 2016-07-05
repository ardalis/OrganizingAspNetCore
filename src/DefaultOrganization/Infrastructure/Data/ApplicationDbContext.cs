using DefaultOrganization.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace DefaultOrganization.Infrastructure.Data
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
