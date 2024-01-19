using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicRPG.Infrastructure.Migrations
{
    public partial class VectorEntityNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalSpawns_VectorEntity_PositionId",
                table: "HospitalSpawns");

            migrationBuilder.AlterColumn<Guid>(
                name: "PositionId",
                table: "HospitalSpawns",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalSpawns_VectorEntity_PositionId",
                table: "HospitalSpawns",
                column: "PositionId",
                principalTable: "VectorEntity",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalSpawns_VectorEntity_PositionId",
                table: "HospitalSpawns");

            migrationBuilder.AlterColumn<Guid>(
                name: "PositionId",
                table: "HospitalSpawns",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalSpawns_VectorEntity_PositionId",
                table: "HospitalSpawns",
                column: "PositionId",
                principalTable: "VectorEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
