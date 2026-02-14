using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialProductAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a3e75b20-c768-4acd-ab5a-8bb294965541"));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("63f9940e-52f0-46d3-ab74-c11ab19d08b0"), new DateTime(2026, 2, 14, 7, 5, 2, 248, DateTimeKind.Utc).AddTicks(6822), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAECDXp4DIYNDO3WI/3ukRxvvQ0Yx0ZrWQ30nlBjXyIiuGP/XY3p1PCSh6Bnzc5CKrYA==", "9999999999", null, null, 2, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("63f9940e-52f0-46d3-ab74-c11ab19d08b0"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("a3e75b20-c768-4acd-ab5a-8bb294965541"), new DateTime(2026, 2, 10, 6, 49, 31, 827, DateTimeKind.Utc).AddTicks(7656), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEOK2ewsqrAGdvNq7402xpy+CA2BtlUrDqY52ffKyZzZ9vUn8XPCOj8j1jULBd9eMbg==", "9999999999", null, null, 2, "admin" });
        }
    }
}
