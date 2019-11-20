using Microsoft.EntityFrameworkCore.Migrations;

namespace APZ_BACKEND.Infrastructure.Data.Migrations
{
    public partial class AddForeignKeysEmployeeRoleItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleItems_EmployeesRoles_EmployeesRoleId",
                table: "EmployeeRoleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleItems_SharedItems_SharedItemId",
                table: "EmployeeRoleItems");

            migrationBuilder.AlterColumn<int>(
                name: "SharedItemId",
                table: "EmployeeRoleItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeesRoleId",
                table: "EmployeeRoleItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoleItems_EmployeesRoles_EmployeesRoleId",
                table: "EmployeeRoleItems",
                column: "EmployeesRoleId",
                principalTable: "EmployeesRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoleItems_SharedItems_SharedItemId",
                table: "EmployeeRoleItems",
                column: "SharedItemId",
                principalTable: "SharedItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleItems_EmployeesRoles_EmployeesRoleId",
                table: "EmployeeRoleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleItems_SharedItems_SharedItemId",
                table: "EmployeeRoleItems");

            migrationBuilder.AlterColumn<int>(
                name: "SharedItemId",
                table: "EmployeeRoleItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EmployeesRoleId",
                table: "EmployeeRoleItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoleItems_EmployeesRoles_EmployeesRoleId",
                table: "EmployeeRoleItems",
                column: "EmployeesRoleId",
                principalTable: "EmployeesRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoleItems_SharedItems_SharedItemId",
                table: "EmployeeRoleItems",
                column: "SharedItemId",
                principalTable: "SharedItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
