using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InveonBootcamp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataInput : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Programming", "Learn the basics of programming using C#.", "Introduction to Programming", 199.99m },
                    { 2, "Programming", "Deep dive into Java for experienced developers.", "Advanced Java", 299.99m },
                    { 3, "Web Development", "Build dynamic websites using ASP.NET Core.", "Web Development with ASP.NET", 249.99m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CourseId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "OrderId", "PaymentDate" },
                values: new object[,]
                {
                    { 1, 199.99m, 1, new DateTime(2024, 12, 27, 15, 50, 2, 378, DateTimeKind.Utc).AddTicks(1599) },
                    { 2, 299.99m, 2, new DateTime(2024, 12, 27, 15, 50, 2, 378, DateTimeKind.Utc).AddTicks(1604) },
                    { 3, 249.99m, 3, new DateTime(2024, 12, 27, 15, 50, 2, 378, DateTimeKind.Utc).AddTicks(1605) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
