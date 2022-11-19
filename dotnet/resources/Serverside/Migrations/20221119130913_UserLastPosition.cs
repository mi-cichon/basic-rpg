using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serverside.Migrations
{
    public partial class UserLastPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_LastPosition",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_LastPosition",
                table: "Users");
        }
    }
}
