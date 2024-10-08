using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReizzzTracking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_Name_To_ToDoSchedule_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "todo_schedule",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "todo_schedule");
        }
    }
}
