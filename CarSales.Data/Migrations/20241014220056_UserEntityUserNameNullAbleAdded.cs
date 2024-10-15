using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSales.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserEntityUserNameNullAbleAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserCreateDate",
                value: new DateTime(2024, 10, 15, 1, 0, 56, 587, DateTimeKind.Local).AddTicks(2318));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserCreateDate",
                value: new DateTime(2024, 10, 14, 22, 58, 50, 93, DateTimeKind.Local).AddTicks(272));
        }
    }
}
