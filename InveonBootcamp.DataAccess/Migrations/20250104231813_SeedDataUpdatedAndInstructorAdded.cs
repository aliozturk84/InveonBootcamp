using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InveonBootcamp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataUpdatedAndInstructorAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Category", "Description", "InstructorId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Programming", "Learn the basics of programming using C#.", 3, "Introduction to Programming", 199.99m },
                    { 2, "Programming", "Deep dive into Java for experienced developers.", 3, "Advanced Java", 299.99m },
                    { 3, "Programming", "Master the fundamentals of Python programming.", 3, "Python for Beginners", 149.99m },
                    { 4, "Web Development", "Build dynamic websites using ASP.NET Core.", 3, "Web Development with ASP.NET", 249.99m },
                    { 5, "Web Development", "Learn the basics of React.js for front-end development.", 3, "React for Beginners", 199.99m },
                    { 6, "Web Development", "Master front-end and back-end development skills.", 3, "Full-Stack Development", 399.99m },
                    { 7, "Data Science", "Explore the world of data science and machine learning.", 3, "Introduction to Data Science", 299.99m },
                    { 8, "Data Science", "Learn how to create stunning data visualizations using Python.", 3, "Data Visualization with Python", 249.99m },
                    { 9, "Data Science", "Dive into the foundations of machine learning.", 3, "Machine Learning Basics", 349.99m },
                    { 10, "Cloud Computing", "Get started with Amazon Web Services for cloud computing.", 3, "Introduction to AWS", 299.99m },
                    { 11, "Cloud Computing", "Learn the basics of Microsoft Azure cloud platform.", 3, "Microsoft Azure Essentials", 279.99m },
                    { 12, "Cloud Computing", "Understand the fundamentals of Google Cloud Platform.", 3, "Google Cloud Platform Fundamentals", 259.99m },
                    { 13, "Cybersecurity", "Learn the fundamentals of securing digital systems.", 3, "Cybersecurity Basics", 199.99m },
                    { 14, "Cybersecurity", "Understand ethical hacking principles and techniques.", 3, "Ethical Hacking", 349.99m },
                    { 15, "Cybersecurity", "Master the concepts of securing network infrastructure.", 3, "Network Security", 299.99m }
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
                    { 1, 199.99m, 1, new DateTime(2025, 1, 4, 23, 18, 12, 848, DateTimeKind.Utc).AddTicks(8560) },
                    { 2, 299.99m, 2, new DateTime(2025, 1, 4, 23, 18, 12, 848, DateTimeKind.Utc).AddTicks(8562) },
                    { 3, 249.99m, 3, new DateTime(2025, 1, 4, 23, 18, 12, 848, DateTimeKind.Utc).AddTicks(8564) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_InstructorId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 15);

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

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Courses");
        }
    }
}
