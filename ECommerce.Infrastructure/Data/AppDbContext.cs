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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
