using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prog7311_Assignment_2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name", "Password", "Surname", "UserId", "Username" },
                values: new object[] { "liam.smith@example.com", "Liam", "liam456", "Smith", 2, "liam.smith" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "EndDate", "Name", "ProductionDate" },
                values: new object[] { "Vegetables", new DateTime(2025, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Organic Carrots", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "EndDate", "FarmerId", "Name", "ProductionDate" },
                values: new object[,]
                {
                    { 2, "Dairy", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Fresh Milk", new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Wheat", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Wheat Flour", new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { "emma.watson@example.com", "Emma", "emma123", "Employee", "Watson", "emma.watson" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name", "Password", "Role", "Username" },
                values: new object[] { "liam.smith@example.com", "Liam", "liam456", "Farmer", "liam.smith" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name", "Password", "Surname", "UserId", "Username" },
                values: new object[] { "james@gmail.com", "James", "Jamie555", "May", 1, "JamesMay555" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "EndDate", "Name", "ProductionDate" },
                values: new object[] { "Food", new DateTime(2025, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apples", new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { "james@gmail.com", "James", "Jamie555", "Farmer", "May", "JamesMay555" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name", "Password", "Role", "Username" },
                values: new object[] { "jane.smith@example.com", "Jane", "pass123", "Employee", "employee1" });
        }
    }
}
