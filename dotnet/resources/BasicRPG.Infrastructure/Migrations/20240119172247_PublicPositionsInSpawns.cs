using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicRPG.Infrastructure.Migrations
{
    public partial class PublicPositionsInSpawns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PositionJson",
                table: "PlayerSpawns",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PositionJson",
                table: "HospitalSpawns",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionJson",
                table: "PlayerSpawns");

            migrationBuilder.DropColumn(
                name: "PositionJson",
                table: "HospitalSpawns");
        }
    }
}
