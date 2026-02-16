using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWishlistTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("63f9940e-52f0-46d3-ab74-c11ab19d08b0"));

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WishlistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("45301f0f-6bbf-46ac-9c70-8ee6dac873e3"), new DateTime(2026, 2, 16, 4, 45, 4, 359, DateTimeKind.Utc).AddTicks(6979), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEIDfo9/RjOF4Dj8x5KZPzsVny3wCK21VUBRv8pSdEt6iBBitOYDuk3NFgT124RUR/g==", "9999999999", null, null, 2, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_WishlistId",
                table: "WishlistItems",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("45301f0f-6bbf-46ac-9c70-8ee6dac873e3"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("63f9940e-52f0-46d3-ab74-c11ab19d08b0"), new DateTime(2026, 2, 14, 7, 5, 2, 248, DateTimeKind.Utc).AddTicks(6822), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAECDXp4DIYNDO3WI/3ukRxvvQ0Yx0ZrWQ30nlBjXyIiuGP/XY3p1PCSh6Bnzc5CKrYA==", "9999999999", null, null, 2, "admin" });
        }
    }
}
