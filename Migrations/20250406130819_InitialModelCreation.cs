using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAndVerify.Migrations
{
    /// <inheritdoc />
    public partial class InitialModelCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChipProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TidMask = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupportsUserMemory = table.Column<bool>(type: "bit", nullable: false),
                    LocksAreShared = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChipProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Readers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RfidTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EPC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChipTypeId = table.Column<int>(type: "int", nullable: false),
                    RssiMin = table.Column<int>(type: "int", nullable: false),
                    RssiMax = table.Column<int>(type: "int", nullable: false),
                    RssiCurrent = table.Column<int>(type: "int", nullable: false),
                    EpcLock = table.Column<int>(type: "int", nullable: true),
                    AccessLock = table.Column<int>(type: "int", nullable: true),
                    KillLock = table.Column<int>(type: "int", nullable: true),
                    UserLock = table.Column<int>(type: "int", nullable: true),
                    ReadTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RfidTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RfidTags_ChipProfile_ChipTypeId",
                        column: x => x.ChipTypeId,
                        principalTable: "ChipProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RfidTags_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Readers_LocationId",
                table: "Readers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RfidTags_ChipTypeId",
                table: "RfidTags",
                column: "ChipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RfidTags_ReaderId",
                table: "RfidTags",
                column: "ReaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RfidTags");

            migrationBuilder.DropTable(
                name: "ChipProfile");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
