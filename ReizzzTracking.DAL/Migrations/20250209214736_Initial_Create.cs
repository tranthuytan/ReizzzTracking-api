using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReizzzTracking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Create : Migration
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
                name: "permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__roles__3214EC07C57C2FD8", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "time_units",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
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
                name: "role_permissions",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_role_permissions_permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permissions_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "time_exchanges",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUnitId = table.Column<long>(type: "bigint", nullable: true),
                    ToUnitId = table.Column<long>(type: "bigint", nullable: true),
                    Multiplier = table.Column<decimal>(type: "decimal(18,10)", nullable: true)
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
                    IsPublic = table.Column<bool>(type: "bit", nullable: true),
                    CopyCount = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "todo_schedule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    AppliedFor = table.Column<long>(type: "bigint", nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: true),
                    EstimatedTime = table.Column<int>(type: "int", nullable: true),
                    ActualTime = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                        principalTable: "roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__user_role__UserI__37A5467C",
                        column: x => x.UserId,
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
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CategoryType = table.Column<long>(type: "bigint", nullable: true),
                    RoutineCollectionId = table.Column<long>(type: "bigint", nullable: true)
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
                        column: x => x.CreatedBy,
                        principalTable: "users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__routines__routinecollections",
                        column: x => x.RoutineCollectionId,
                        principalTable: "routine_collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "AddToDo" },
                    { 2L, "ReadToDo" },
                    { 3L, "UpdateToDo" },
                    { 4L, "DeleteToDo" },
                    { 5L, "AddRoutine" },
                    { 6L, "ReadRoutine" },
                    { 7L, "UpdateRoutine" },
                    { 8L, "DeleteRoutine" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "Registered" });

            migrationBuilder.InsertData(
                table: "time_units",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Second" },
                    { 2L, "Minute" },
                    { 3L, "Hour" }
                });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 1L },
                    { 3L, 1L },
                    { 4L, 1L },
                    { 5L, 1L },
                    { 6L, 1L },
                    { 7L, 1L },
                    { 8L, 1L }
                });

            migrationBuilder.InsertData(
                table: "time_exchanges",
                columns: new[] { "Id", "FromUnitId", "Multiplier", "ToUnitId" },
                values: new object[,]
                {
                    { 1L, 1L, 0.0166666666666666666666666667m, 2L },
                    { 2L, 1L, 0.0002777777777777777777777778m, 3L },
                    { 3L, 2L, 60m, 1L },
                    { 4L, 2L, 0.0166666666666666666666666667m, 3L },
                    { 5L, 3L, 3600m, 1L },
                    { 6L, 3L, 60m, 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_PermissionId",
                table: "role_permissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "UQ__roles__737584F61C4A63D0",
                table: "roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_routine_collections_CreatedBy",
                table: "routine_collections",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_routines_CategoryType",
                table: "routines",
                column: "CategoryType");

            migrationBuilder.CreateIndex(
                name: "IX_routines_CreatedBy",
                table: "routines",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_routines_RoutineCollectionId",
                table: "routines",
                column: "RoutineCollectionId");

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
                name: "UQ__users__536C85E4AB2DC2C6",
                table: "users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "routines");

            migrationBuilder.DropTable(
                name: "time_exchanges");

            migrationBuilder.DropTable(
                name: "todo_schedule");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "routine_collections");

            migrationBuilder.DropTable(
                name: "category_types");

            migrationBuilder.DropTable(
                name: "time_units");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
