using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReizzzTracking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Remove_UserTask_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_tasks");

            migrationBuilder.DropColumn(
                name: "ToDoId",
                table: "todo_schedule");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ToDoId",
                table: "todo_schedule",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user_tasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppliedFor = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_tas__3214EC0768926AE1", x => x.Id);
                    table.ForeignKey(
                        name: "FK__user_task__Appli__398D8EEE",
                        column: x => x.AppliedFor,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_tasks_AppliedFor",
                table: "user_tasks",
                column: "AppliedFor");
        }
    }
}
