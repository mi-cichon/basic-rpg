using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicRPG.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Money = table.Column<double>(type: "double precision", nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastPositionJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VectorEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    X = table.Column<float>(type: "real", nullable: false),
                    Y = table.Column<float>(type: "real", nullable: false),
                    Z = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VectorEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalSpawns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Heading = table.Column<float>(type: "real", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalSpawns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalSpawns_VectorEntity_PositionId",
                        column: x => x.PositionId,
                        principalTable: "VectorEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSpawns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SpawnLocation = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSpawns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerSpawns_VectorEntity_PositionId",
                        column: x => x.PositionId,
                        principalTable: "VectorEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HospitalSpawns_PositionId",
                table: "HospitalSpawns",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSpawns_PositionId",
                table: "PlayerSpawns",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalSpawns");

            migrationBuilder.DropTable(
                name: "PlayerSpawns");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VectorEntity");
        }
    }
}
