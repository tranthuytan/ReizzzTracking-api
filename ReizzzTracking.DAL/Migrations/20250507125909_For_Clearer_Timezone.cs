using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReizzzTracking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class For_Clearer_Timezone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartAt",
                table: "todo_schedule",
                newName: "StartAtUtc");

            migrationBuilder.RenameColumn(
                name: "EndAt",
                table: "todo_schedule",
                newName: "EndAtUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartAtUtc",
                table: "todo_schedule",
                newName: "StartAt");

            migrationBuilder.RenameColumn(
                name: "EndAtUtc",
                table: "todo_schedule",
                newName: "EndAt");
        }
    }
}
