using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory2.API.Migrations
{
    public partial class Init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Inventories_InventoryId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "IventoryId",
                table: "Receipts");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "Receipts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Deliveries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_InventoryId",
                table: "Deliveries",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Inventories_InventoryId",
                table: "Deliveries",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Inventories_InventoryId",
                table: "Receipts",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Inventories_InventoryId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Inventories_InventoryId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_InventoryId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Deliveries");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "Receipts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "IventoryId",
                table: "Receipts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Inventories_InventoryId",
                table: "Receipts",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
