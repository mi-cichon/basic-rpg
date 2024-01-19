using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicRPG.Infrastructure.Migrations
{
    public partial class AddedSpawns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_LastPosition",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "HospitalSpawns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Heading = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalSpawns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSpawns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SpawnLocation = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSpawns", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalSpawns");

            migrationBuilder.DropTable(
                name: "PlayerSpawns");

            migrationBuilder.AddColumn<string>(
                name: "_LastPosition",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
