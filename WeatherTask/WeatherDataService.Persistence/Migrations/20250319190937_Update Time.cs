using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherDataService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Weather");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Weather",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "Weather",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Weather");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Weather");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Weather",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
