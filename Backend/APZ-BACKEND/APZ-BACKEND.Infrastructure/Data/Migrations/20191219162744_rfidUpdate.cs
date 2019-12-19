using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APZ_BACKEND.Infrastructure.Data.Migrations
{
    public partial class rfidUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RfidNumber",
                table: "SharedItems",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RfidNumber",
                table: "PrivateUsers",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RfidNumber",
                table: "SharedItems",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "RfidNumber",
                table: "PrivateUsers",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
