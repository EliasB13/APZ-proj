using Microsoft.EntityFrameworkCore.Migrations;

namespace APZ_BACKEND.Infrastructure.Data.Migrations
{
    public partial class addedAdminProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "PrivateUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "PrivateUsers");
        }
    }
}
