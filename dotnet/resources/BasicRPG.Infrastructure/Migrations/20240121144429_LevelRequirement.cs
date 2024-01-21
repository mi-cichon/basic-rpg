using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicRPG.Infrastructure.Migrations
{
    public partial class LevelRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LevelRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequiredLevel = table.Column<int>(type: "integer", nullable: false),
                    RequiredExp = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelRequirements", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LevelRequirements");
        }
    }
}
