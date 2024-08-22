using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReizzzTracking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Update_Roles_and_add_Permission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category_types",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__category__3214EC07ADA88A03", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "time_units",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__time_uni__3214EC0723133C3F", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3214EC07F61BA3CE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "time_exchanges",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUnitId = table.Column<long>(type: "bigint", nullable: true),
                    ToUnitId = table.Column<long>(type: "bigint", nullable: true),
                    Multiplier = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__time_exc__3214EC07B60632EC", x => x.Id);
                    table.ForeignKey(
                        name: "FK__time_exch__FromU__412EB0B6",
                        column: x => x.FromUnitId,
                        principalTable: "time_units",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__time_exch__ToUni__4222D4EF",
                        column: x => x.ToUnitId,
                        principalTable: "time_units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "routine_collections",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__routine___3214EC071AF97170", x => x.Id);
                    table.ForeignKey(
                        name: "FK__routine_c__Creat__3A81B327",
                        column: x => x.CreatedBy,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "routines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: true),
                    UsedBy = table.Column<long>(type: "bigint", nullable: true),
                    CategoryType = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__routines__3214EC07877522F1", x => x.Id);
                    table.ForeignKey(
                        name: "FK__routines__Catego__3D5E1FD2",
                        column: x => x.CategoryType,
                        principalTable: "category_types",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__routines__UsedBy__3C69FB99",
                        column: x => x.UsedBy,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "todo_schedule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToDoId = table.Column<long>(type: "bigint", nullable: true),
                    AppliedFor = table.Column<long>(type: "bigint", nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: true),
                    EstimatedTime = table.Column<int>(type: "int", nullable: true),
                    ActualTime = table.Column<int>(type: "int", nullable: true),
                    TimeUnitId = table.Column<long>(type: "bigint", nullable: true),
                    CategoryType = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__todo_sch__3214EC07ECF0E426", x => x.Id);
                    table.ForeignKey(
                        name: "FK__todo_sche__Appli__3E52440B",
                        column: x => x.AppliedFor,
                        principalTable: "users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__todo_sche__Categ__403A8C7D",
                        column: x => x.CategoryType,
                        principalTable: "category_types",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__todo_sche__TimeU__3F466844",
                        column: x => x.TimeUnitId,
                        principalTable: "time_units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_rol__F8E69A0D2F69DE35", x => new { x.Id, x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK__user_role__RoleI__38996AB5",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__user_role__UserI__37A5467C",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user_tasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AppliedFor = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
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
                name: "IX_Permission_RoleId",
                table: "Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_routine_collections_CreatedBy",
                table: "routine_collections",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_routines_CategoryType",
                table: "routines",
                column: "CategoryType");

            migrationBuilder.CreateIndex(
                name: "IX_routines_UsedBy",
                table: "routines",
                column: "UsedBy");

            migrationBuilder.CreateIndex(
                name: "IX_time_exchanges_FromUnitId",
                table: "time_exchanges",
                column: "FromUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_time_exchanges_ToUnitId",
                table: "time_exchanges",
                column: "ToUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_todo_schedule_AppliedFor",
                table: "todo_schedule",
                column: "AppliedFor");

            migrationBuilder.CreateIndex(
                name: "IX_todo_schedule_CategoryType",
                table: "todo_schedule",
                column: "CategoryType");

            migrationBuilder.CreateIndex(
                name: "IX_todo_schedule_TimeUnitId",
                table: "todo_schedule",
                column: "TimeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_RoleId",
                table: "user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_UserId",
                table: "user_roles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_tasks_AppliedFor",
                table: "user_tasks",
                column: "AppliedFor");

            migrationBuilder.CreateIndex(
                name: "UQ__users__536C85E4AB2DC2C6",
                table: "users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "routine_collections");

            migrationBuilder.DropTable(
                name: "routines");

            migrationBuilder.DropTable(
                name: "time_exchanges");

            migrationBuilder.DropTable(
                name: "todo_schedule");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tasks");

            migrationBuilder.DropTable(
                name: "category_types");

            migrationBuilder.DropTable(
                name: "time_units");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
