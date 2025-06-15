using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAndVerify.Migrations
{
    /// <inheritdoc />
    public partial class ModifyiedLocationAndOthers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Readers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirmwareVersion",
                table: "Readers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSeenAt",
                table: "Readers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Readers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Locations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Locations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Readers");

            migrationBuilder.DropColumn(
                name: "FirmwareVersion",
                table: "Readers");

            migrationBuilder.DropColumn(
                name: "LastSeenAt",
                table: "Readers");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Readers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Locations");
        }
    }
}
