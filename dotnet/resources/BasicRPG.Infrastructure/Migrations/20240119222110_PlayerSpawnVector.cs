using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicRPG.Infrastructure.Migrations
{
    public partial class PlayerSpawnVector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionJson",
                table: "PlayerSpawns");

            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "PlayerSpawns",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSpawns_PositionId",
                table: "PlayerSpawns",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerSpawns_VectorEntity_PositionId",
                table: "PlayerSpawns",
                column: "PositionId",
                principalTable: "VectorEntity",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerSpawns_VectorEntity_PositionId",
                table: "PlayerSpawns");

            migrationBuilder.DropIndex(
                name: "IX_PlayerSpawns_PositionId",
                table: "PlayerSpawns");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "PlayerSpawns");

            migrationBuilder.AddColumn<string>(
                name: "PositionJson",
                table: "PlayerSpawns",
                type: "text",
                nullable: true);
        }
    }
}
