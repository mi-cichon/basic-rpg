using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicRPG.Infrastructure.Migrations
{
    public partial class AddedLastPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastPositionJson",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPositionJson",
                table: "Users");
        }
    }
}
