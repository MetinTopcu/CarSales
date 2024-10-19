using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSales.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserGuidAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserGuid",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "UserCreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 10, 16, 1, 55, 28, 224, DateTimeKind.Local).AddTicks(2058), new Guid("f3c1a0ec-35d7-46dd-a9da-0e8238fe4588") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserCreateDate",
                value: new DateTime(2024, 10, 15, 1, 0, 56, 587, DateTimeKind.Local).AddTicks(2318));
        }
    }
}
