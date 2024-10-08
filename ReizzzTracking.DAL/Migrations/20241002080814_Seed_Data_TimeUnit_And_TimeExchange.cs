using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReizzzTracking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Data_TimeUnit_And_TimeExchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "time_units",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Multiplier",
                table: "time_exchanges",
                type: "decimal(18,10)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "time_exchanges",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "time_exchanges",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "time_exchanges",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "time_exchanges",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "time_exchanges",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "time_exchanges",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "time_units",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "time_units",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "time_units",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Name",
                table: "time_units",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Multiplier",
                table: "time_exchanges",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,10)",
                oldNullable: true);
        }
    }
}
