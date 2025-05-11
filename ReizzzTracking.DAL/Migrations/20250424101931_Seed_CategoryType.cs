using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReizzzTracking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Seed_CategoryType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "category_types");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "category_types",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "category_types",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Routine" },
                    { 2L, "ToDo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "category_types",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "category_types",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "category_types");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "category_types",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
