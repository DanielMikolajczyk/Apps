using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apps.Data.Migrations
{
    public partial class ActApplicationUseradditionalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "ActApplicationUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Vote",
                table: "ActApplicationUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "ActApplicationUsers");

            migrationBuilder.DropColumn(
                name: "Vote",
                table: "ActApplicationUsers");
        }
    }
}
