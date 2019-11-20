using Microsoft.EntityFrameworkCore.Migrations;

namespace APZ_BACKEND.Infrastructure.Data.Migrations
{
    public partial class addEmployeeForeignKeys1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BusinessUsers_BusinessUserId1",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_PrivateUsers_PrivateUserId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BusinessUserId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PrivateUserId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BusinessUserId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PrivateUserId1",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "BusinessUserId",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrivateUserId",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BusinessUserId",
                table: "Employees",
                column: "BusinessUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PrivateUserId",
                table: "Employees",
                column: "PrivateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BusinessUsers_BusinessUserId",
                table: "Employees",
                column: "BusinessUserId",
                principalTable: "BusinessUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_PrivateUsers_PrivateUserId",
                table: "Employees",
                column: "PrivateUserId",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BusinessUsers_BusinessUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_PrivateUsers_PrivateUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BusinessUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PrivateUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BusinessUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PrivateUserId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "BusinessUserId1",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrivateUserId1",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BusinessUserId1",
                table: "Employees",
                column: "BusinessUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PrivateUserId1",
                table: "Employees",
                column: "PrivateUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BusinessUsers_BusinessUserId1",
                table: "Employees",
                column: "BusinessUserId1",
                principalTable: "BusinessUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_PrivateUsers_PrivateUserId1",
                table: "Employees",
                column: "PrivateUserId1",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
