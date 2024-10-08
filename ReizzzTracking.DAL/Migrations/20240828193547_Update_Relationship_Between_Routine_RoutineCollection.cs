using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReizzzTracking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Update_Relationship_Between_Routine_RoutineCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RoutineCollectionId",
                table: "routines",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_routines_RoutineCollectionId",
                table: "routines",
                column: "RoutineCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK__routines__routinecollections",
                table: "routines",
                column: "RoutineCollectionId",
                principalTable: "routine_collections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__routines__routinecollections",
                table: "routines");

            migrationBuilder.DropIndex(
                name: "IX_routines_RoutineCollectionId",
                table: "routines");

            migrationBuilder.DropColumn(
                name: "RoutineCollectionId",
                table: "routines");
        }
    }
}
