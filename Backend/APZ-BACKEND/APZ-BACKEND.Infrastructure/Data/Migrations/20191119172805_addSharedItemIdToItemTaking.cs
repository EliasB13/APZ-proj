using Microsoft.EntityFrameworkCore.Migrations;

namespace APZ_BACKEND.Infrastructure.Data.Migrations
{
    public partial class addSharedItemIdToItemTaking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemTakingLines_SharedItems_SharedItemId",
                table: "ItemTakingLines");

            migrationBuilder.AlterColumn<int>(
                name: "SharedItemId",
                table: "ItemTakingLines",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTakingLines_SharedItems_SharedItemId",
                table: "ItemTakingLines",
                column: "SharedItemId",
                principalTable: "SharedItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemTakingLines_SharedItems_SharedItemId",
                table: "ItemTakingLines");

            migrationBuilder.AlterColumn<int>(
                name: "SharedItemId",
                table: "ItemTakingLines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTakingLines_SharedItems_SharedItemId",
                table: "ItemTakingLines",
                column: "SharedItemId",
                principalTable: "SharedItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
