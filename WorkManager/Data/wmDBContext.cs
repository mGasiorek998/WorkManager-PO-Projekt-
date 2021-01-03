using System.Data.Entity;

namespace WorkManager.Models
{
    class wmDBContext : DbContext{

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
