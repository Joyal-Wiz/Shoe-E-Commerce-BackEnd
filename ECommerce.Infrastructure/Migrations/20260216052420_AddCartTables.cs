using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCartTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("45301f0f-6bbf-46ac-9c70-8ee6dac873e3"));

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("918744c0-8fdf-4b03-83ff-6ca4f05d5026"), new DateTime(2026, 2, 16, 5, 24, 20, 179, DateTimeKind.Utc).AddTicks(3854), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEGjds9rEyKcbmnZmv1eDO/YdYKWFp2kioc/rJvFrjRoApqE5O8znLs5J9PcuGAY9UQ==", "9999999999", null, null, 2, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("918744c0-8fdf-4b03-83ff-6ca4f05d5026"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("45301f0f-6bbf-46ac-9c70-8ee6dac873e3"), new DateTime(2026, 2, 16, 4, 45, 4, 359, DateTimeKind.Utc).AddTicks(6979), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEIDfo9/RjOF4Dj8x5KZPzsVny3wCK21VUBRv8pSdEt6iBBitOYDuk3NFgT124RUR/g==", "9999999999", null, null, 2, "admin" });
        }
    }
}
