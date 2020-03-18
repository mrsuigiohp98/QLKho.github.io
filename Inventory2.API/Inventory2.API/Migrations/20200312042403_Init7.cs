using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory2.API.Migrations
{
    public partial class Init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Receipts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IventoryId",
                table: "Receipts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_InventoryId",
                table: "Receipts",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Inventories_InventoryId",
                table: "Receipts",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Inventories_InventoryId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_InventoryId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "IventoryId",
                table: "Receipts");
        }
    }
}
