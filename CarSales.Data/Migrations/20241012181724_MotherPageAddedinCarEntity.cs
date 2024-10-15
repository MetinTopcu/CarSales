using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSales.Data.Migrations
{
    /// <inheritdoc />
    public partial class MotherPageAddedinCarEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForMotherPage",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserCreateDate",
                value: new DateTime(2024, 10, 12, 21, 17, 24, 114, DateTimeKind.Local).AddTicks(986));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForMotherPage",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserCreateDate",
                value: new DateTime(2024, 10, 9, 3, 10, 48, 701, DateTimeKind.Local).AddTicks(7956));
        }
    }
}
