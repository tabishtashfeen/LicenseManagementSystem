using LicenseManagementSystem.Models.License;
using LicenseManagementSystem.Models.Product;
using LicenseManagementSystem.Models.User;
using Microsoft.EntityFrameworkCore;

namespace LicenseManagementSystem.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<License> Licenses { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var adminUser = new User
            {
                Id = 0001,
                UserName = "admin@lms.com",
                Email = "admin@lms.com",
                FirstName = "Admin",
                LastName = "User",
                Role = "Admin",
                Password = "00001111"
            };

            builder.Entity<User>().HasData(adminUser);
        }
    }
}
