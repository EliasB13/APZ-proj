using Microsoft.EntityFrameworkCore.Migrations;

namespace APZ_BACKEND.Infrastructure.Data.Migrations
{
    public partial class usersUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "PrivateUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "BusinessUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "PrivateUsers");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "BusinessUsers");
        }
    }
}
