using Microsoft.EntityFrameworkCore.Migrations;

namespace APZ_BACKEND.Infrastructure.Data.Migrations
{
    public partial class fixKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleItems_EmployeesRoles_EmployeesRoleId",
                table: "EmployeeRoleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleItems_SharedItems_SharedItemId",
                table: "EmployeeRoleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_PrivateUsers_PrivateUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesRoles_BusinessUsers_BusinessUserId",
                table: "EmployeesRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTakingLines_ItemTakings_ItemTakingId",
                table: "ItemTakingLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTakings_PrivateUsers_PrivateUserId",
                table: "ItemTakings");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedItems_BusinessUsers_BusinessUserId",
                table: "SharedItems");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessUserId",
                table: "SharedItems",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PrivateUserId",
                table: "ItemTakings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ItemTakingId",
                table: "ItemTakingLines",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessUserId",
                table: "EmployeesRoles",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PrivateUserId",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SharedItemId",
                table: "EmployeeRoleItems",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeesRoleId",
                table: "EmployeeRoleItems",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_PrivateUsers_PrivateUserId",
                table: "Employees",
                column: "PrivateUserId",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesRoles_BusinessUsers_BusinessUserId",
                table: "EmployeesRoles",
                column: "BusinessUserId",
                principalTable: "BusinessUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTakingLines_ItemTakings_ItemTakingId",
                table: "ItemTakingLines",
                column: "ItemTakingId",
                principalTable: "ItemTakings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTakings_PrivateUsers_PrivateUserId",
                table: "ItemTakings",
                column: "PrivateUserId",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedItems_BusinessUsers_BusinessUserId",
                table: "SharedItems",
                column: "BusinessUserId",
                principalTable: "BusinessUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleItems_EmployeesRoles_EmployeesRoleId",
                table: "EmployeeRoleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleItems_SharedItems_SharedItemId",
                table: "EmployeeRoleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_PrivateUsers_PrivateUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesRoles_BusinessUsers_BusinessUserId",
                table: "EmployeesRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTakingLines_ItemTakings_ItemTakingId",
                table: "ItemTakingLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTakings_PrivateUsers_PrivateUserId",
                table: "ItemTakings");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedItems_BusinessUsers_BusinessUserId",
                table: "SharedItems");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessUserId",
                table: "SharedItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrivateUserId",
                table: "ItemTakings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemTakingId",
                table: "ItemTakingLines",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BusinessUserId",
                table: "EmployeesRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrivateUserId",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SharedItemId",
                table: "EmployeeRoleItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeesRoleId",
                table: "EmployeeRoleItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
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

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_PrivateUsers_PrivateUserId",
                table: "Employees",
                column: "PrivateUserId",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesRoles_BusinessUsers_BusinessUserId",
                table: "EmployeesRoles",
                column: "BusinessUserId",
                principalTable: "BusinessUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTakingLines_ItemTakings_ItemTakingId",
                table: "ItemTakingLines",
                column: "ItemTakingId",
                principalTable: "ItemTakings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTakings_PrivateUsers_PrivateUserId",
                table: "ItemTakings",
                column: "PrivateUserId",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedItems_BusinessUsers_BusinessUserId",
                table: "SharedItems",
                column: "BusinessUserId",
                principalTable: "BusinessUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
