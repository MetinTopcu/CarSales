using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSales.Data.Migrations
{
    /// <inheritdoc />
    public partial class CarImageAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Cars",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Cars",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Cars",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "Cars",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image5",
                table: "Cars",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserCreateDate",
                value: new DateTime(2024, 10, 9, 3, 3, 35, 737, DateTimeKind.Local).AddTicks(3564));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Image5",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserCreateDate",
                value: new DateTime(2024, 9, 30, 19, 31, 48, 258, DateTimeKind.Local).AddTicks(3793));
        }
    }
}
