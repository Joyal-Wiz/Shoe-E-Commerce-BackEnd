using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8ace17e1-43b4-4ebc-a3c9-b63a71ff1984"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("4f904a66-031b-49ba-a4a9-633d5a012cd5"), new DateTime(2026, 2, 24, 9, 38, 39, 555, DateTimeKind.Utc).AddTicks(5695), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEDXncaieiyv1eGlaAAxMUsdJlp8lGTmaBASkbC/Z/GDPCtKMexAjMo6DoCixOzlCvQ==", "9999999999", null, null, 2, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4f904a66-031b-49ba-a4a9-633d5a012cd5"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("8ace17e1-43b4-4ebc-a3c9-b63a71ff1984"), new DateTime(2026, 2, 20, 7, 21, 56, 127, DateTimeKind.Utc).AddTicks(4985), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEAQ/MtzeRFUrbWo6bBjhKNh1h3UDJsNaN7HmwXjS1uYwKK8spasMR+HpshdUKHQJVA==", "9999999999", null, null, 2, "admin" });
        }
    }
}
