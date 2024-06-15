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
    }
}
