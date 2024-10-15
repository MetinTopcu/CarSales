using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSales.Data.Migrations
{
    /// <inheritdoc />
    public partial class SliderAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserCreateDate",
                value: new DateTime(2024, 10, 9, 3, 10, 48, 701, DateTimeKind.Local).AddTicks(7956));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserCreateDate",
                value: new DateTime(2024, 10, 9, 3, 3, 35, 737, DateTimeKind.Local).AddTicks(3564));
        }
    }
}
