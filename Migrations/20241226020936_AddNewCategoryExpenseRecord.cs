using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTrackerv1.Migrations
{
    public partial class AddNewCategoryExpenseRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "ExpenseId", "Amount", "CategoryId", "ExpenseDate", "UserId" },
                values: new object[] { 1, 12.5, 1, new DateTime(2024, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "ExpenseId",
                keyValue: 1);
        }
    }
}
