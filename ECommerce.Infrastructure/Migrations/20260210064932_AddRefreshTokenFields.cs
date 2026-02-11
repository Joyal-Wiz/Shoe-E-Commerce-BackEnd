using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dcf9b793-25ea-4405-a141-f07a9eacc3db"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("a3e75b20-c768-4acd-ab5a-8bb294965541"), new DateTime(2026, 2, 10, 6, 49, 31, 827, DateTimeKind.Utc).AddTicks(7656), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEOK2ewsqrAGdvNq7402xpy+CA2BtlUrDqY52ffKyZzZ9vUn8XPCOj8j1jULBd9eMbg==", "9999999999", null, null, 2, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a3e75b20-c768-4acd-ab5a-8bb294965541"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "Role", "Username" },
                values: new object[] { new Guid("dcf9b793-25ea-4405-a141-f07a9eacc3db"), new DateTime(2026, 2, 10, 5, 12, 54, 176, DateTimeKind.Utc).AddTicks(3950), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEIv7c9/eoJ8oVK53E3TaalFLlUqLZ7VplE77amKwuSgdJh+VLHzVddYeY69Wz1ppeA==", "9999999999", 2, "admin" });
        }
    }
}
