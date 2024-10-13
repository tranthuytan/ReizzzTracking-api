using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReizzzTracking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_DeleteCascadeBehavior_Routine_RoutineCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__routines__routinecollections",
                table: "routines");

            migrationBuilder.AddForeignKey(
                name: "FK__routines__routinecollections",
                table: "routines",
                column: "RoutineCollectionId",
                principalTable: "routine_collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__routines__routinecollections",
                table: "routines");

            migrationBuilder.AddForeignKey(
                name: "FK__routines__routinecollections",
                table: "routines",
                column: "RoutineCollectionId",
                principalTable: "routine_collections",
                principalColumn: "Id");
        }
    }
}
