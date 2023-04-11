using Microsoft.EntityFrameworkCore;
using testTask.Data.Models;

namespace testTask.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<Sight> Sights { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
