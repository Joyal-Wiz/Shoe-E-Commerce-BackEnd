using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            var passwordService = new PasswordService();

            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Username = "admin",
                Email = "admin@ecommerce.com",
                PhoneNo = "9999999999",
                Role = UserRole.Admin,
                IsActive = true,
                PasswordHash = passwordService.HashPassword("Admin@123")
            };

            modelBuilder.Entity<User>().HasData(adminUser);
        }

    }
}
