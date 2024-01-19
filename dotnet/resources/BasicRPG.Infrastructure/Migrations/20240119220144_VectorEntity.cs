using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicRPG.Infrastructure.Migrations
{
    public partial class VectorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionJson",
                table: "HospitalSpawns");

            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "HospitalSpawns",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_HospitalSpawns_PositionId",
                table: "HospitalSpawns",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalSpawns_VectorEntity_PositionId",
                table: "HospitalSpawns",
                column: "PositionId",
                principalTable: "VectorEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalSpawns_VectorEntity_PositionId",
                table: "HospitalSpawns");

            migrationBuilder.DropTable(
                name: "VectorEntity");

            migrationBuilder.DropIndex(
                name: "IX_HospitalSpawns_PositionId",
                table: "HospitalSpawns");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "HospitalSpawns");

            migrationBuilder.AddColumn<string>(
                name: "PositionJson",
                table: "HospitalSpawns",
                type: "text",
                nullable: true);
        }
    }
}
