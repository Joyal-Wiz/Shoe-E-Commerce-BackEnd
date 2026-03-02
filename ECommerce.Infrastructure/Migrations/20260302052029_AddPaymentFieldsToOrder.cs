using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentFieldsToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4f904a66-031b-49ba-a4a9-633d5a012cd5"));

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentVerifiedAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazorpayOrderId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazorpayPaymentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("f823ccd3-cbb3-4ef7-8128-7e3258e4180b"), new DateTime(2026, 3, 2, 5, 20, 29, 215, DateTimeKind.Utc).AddTicks(4007), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEFojF9YoDFATYnvdpkyTIgo4ChjCm1Art/3y0KYQJ9LSpOzDMtsoLKG5q6QGwgQhvA==", "9999999999", null, null, 2, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f823ccd3-cbb3-4ef7-8128-7e3258e4180b"));

            migrationBuilder.DropColumn(
                name: "PaymentVerifiedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RazorpayOrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RazorpayPaymentId",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "PhoneNo", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { new Guid("4f904a66-031b-49ba-a4a9-633d5a012cd5"), new DateTime(2026, 2, 24, 9, 38, 39, 555, DateTimeKind.Utc).AddTicks(5695), "admin@ecommerce.com", true, "Admin", "AQAAAAIAAYagAAAAEDXncaieiyv1eGlaAAxMUsdJlp8lGTmaBASkbC/Z/GDPCtKMexAjMo6DoCixOzlCvQ==", "9999999999", null, null, 2, "admin" });
        }
    }
}
