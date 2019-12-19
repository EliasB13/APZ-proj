using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APZ_BACKEND.Infrastructure.Data.Migrations
{
    public partial class addingReaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReaderId",
                table: "SharedItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecretHash = table.Column<byte[]>(nullable: true),
                    SecretSalt = table.Column<byte[]>(nullable: true),
                    BusinessUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reader_BusinessUsers_BusinessUserId",
                        column: x => x.BusinessUserId,
                        principalTable: "BusinessUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedItems_ReaderId",
                table: "SharedItems",
                column: "ReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reader_BusinessUserId",
                table: "Reader",
                column: "BusinessUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedItems_Reader_ReaderId",
                table: "SharedItems",
                column: "ReaderId",
                principalTable: "Reader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedItems_Reader_ReaderId",
                table: "SharedItems");

            migrationBuilder.DropTable(
                name: "Reader");

            migrationBuilder.DropIndex(
                name: "IX_SharedItems_ReaderId",
                table: "SharedItems");

            migrationBuilder.DropColumn(
                name: "ReaderId",
                table: "SharedItems");
        }
    }
}
