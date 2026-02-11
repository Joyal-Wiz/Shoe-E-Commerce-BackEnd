using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "Role", "Username" },
                values: new object[] { new Guid("dcf9b793-25ea-4405-a141-f07a9eacc3db"), new DateTime(2026, 2, 10, 5, 12, 54, 176, DateTimeKind.Utc).AddTicks(3950), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEIv7c9/eoJ8oVK53E3TaalFLlUqLZ7VplE77amKwuSgdJh+VLHzVddYeY69Wz1ppeA==", "9999999999", 2, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dcf9b793-25ea-4405-a141-f07a9eacc3db"));
        }
    }
}
