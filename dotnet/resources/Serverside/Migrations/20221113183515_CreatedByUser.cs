using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serverside.Migrations
{
    public partial class CreatedByUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CreatedBy",
                table: "Users",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");
        }
    }
}
