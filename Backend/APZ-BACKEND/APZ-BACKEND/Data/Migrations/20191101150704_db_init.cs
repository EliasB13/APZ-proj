using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APZ_BACKEND.Migrations
{
    public partial class db_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    IsEmailConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    IsEmailConfirmed = table.Column<bool>(nullable: false),
                    RfidNumber = table.Column<byte[]>(nullable: true),
                    SearchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BusinessUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeesRoles_BusinessUsers_BusinessUserId",
                        column: x => x.BusinessUserId,
                        principalTable: "BusinessUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SharedItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RfidNumber = table.Column<byte[]>(nullable: true),
                    BusinessUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedItems_BusinessUsers_BusinessUserId",
                        column: x => x.BusinessUserId,
                        principalTable: "BusinessUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemTakings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TakingTime = table.Column<DateTime>(nullable: false),
                    PrivateUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTakings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTakings_PrivateUsers_PrivateUserId",
                        column: x => x.PrivateUserId,
                        principalTable: "PrivateUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrivateUserId = table.Column<int>(nullable: true),
                    BusinessUserId = table.Column<int>(nullable: true),
                    EmployeesRoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_BusinessUsers_BusinessUserId",
                        column: x => x.BusinessUserId,
                        principalTable: "BusinessUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeesRoles_EmployeesRoleId",
                        column: x => x.EmployeesRoleId,
                        principalTable: "EmployeesRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_PrivateUsers_PrivateUserId",
                        column: x => x.PrivateUserId,
                        principalTable: "PrivateUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoleItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SharedItemId = table.Column<int>(nullable: true),
                    EmployeesRoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeRoleItems_EmployeesRoles_EmployeesRoleId",
                        column: x => x.EmployeesRoleId,
                        principalTable: "EmployeesRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRoleItems_SharedItems_SharedItemId",
                        column: x => x.SharedItemId,
                        principalTable: "SharedItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemTakingLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsReturned = table.Column<bool>(nullable: false),
                    ReturningTime = table.Column<DateTime>(nullable: false),
                    ItemTakingId = table.Column<int>(nullable: true),
                    SharedItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTakingLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTakingLines_ItemTakings_ItemTakingId",
                        column: x => x.ItemTakingId,
                        principalTable: "ItemTakings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemTakingLines_SharedItems_SharedItemId",
                        column: x => x.SharedItemId,
                        principalTable: "SharedItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoleItems_EmployeesRoleId",
                table: "EmployeeRoleItems",
                column: "EmployeesRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoleItems_SharedItemId",
                table: "EmployeeRoleItems",
                column: "SharedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BusinessUserId",
                table: "Employees",
                column: "BusinessUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeesRoleId",
                table: "Employees",
                column: "EmployeesRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PrivateUserId",
                table: "Employees",
                column: "PrivateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesRoles_BusinessUserId",
                table: "EmployeesRoles",
                column: "BusinessUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTakingLines_ItemTakingId",
                table: "ItemTakingLines",
                column: "ItemTakingId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTakingLines_SharedItemId",
                table: "ItemTakingLines",
                column: "SharedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTakings_PrivateUserId",
                table: "ItemTakings",
                column: "PrivateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedItems_BusinessUserId",
                table: "SharedItems",
                column: "BusinessUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRoleItems");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ItemTakingLines");

            migrationBuilder.DropTable(
                name: "EmployeesRoles");

            migrationBuilder.DropTable(
                name: "ItemTakings");

            migrationBuilder.DropTable(
                name: "SharedItems");

            migrationBuilder.DropTable(
                name: "PrivateUsers");

            migrationBuilder.DropTable(
                name: "BusinessUsers");
        }
    }
}
