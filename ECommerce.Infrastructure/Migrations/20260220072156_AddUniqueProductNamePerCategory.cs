using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueProductNamePerCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("73885e84-3be5-4e78-82fa-0678082b42be"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("8ace17e1-43b4-4ebc-a3c9-b63a71ff1984"), new DateTime(2026, 2, 20, 7, 21, 56, 127, DateTimeKind.Utc).AddTicks(4985), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEAQ/MtzeRFUrbWo6bBjhKNh1h3UDJsNaN7HmwXjS1uYwKK8spasMR+HpshdUKHQJVA==", "9999999999", null, null, 2, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name_CategoryId",
                table: "Products",
                columns: new[] { "Name", "CategoryId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Name_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8ace17e1-43b4-4ebc-a3c9-b63a71ff1984"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("73885e84-3be5-4e78-82fa-0678082b42be"), new DateTime(2026, 2, 16, 9, 53, 7, 926, DateTimeKind.Utc).AddTicks(875), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEMnfBVFnXMasvI2w23+YmqDwFTg0Xm8xMom30O+3XDiEQ1uwX3j8xxMEVGhZbKzdSw==", "9999999999", null, null, 2, "admin" });
        }
    }
}
