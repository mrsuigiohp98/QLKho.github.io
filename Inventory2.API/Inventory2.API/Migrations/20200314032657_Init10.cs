using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory2.API.Migrations
{
    public partial class Init10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Receipts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Deliveries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_SupplierId",
                table: "Receipts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CustomerId",
                table: "Deliveries",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Customers_CustomerId",
                table: "Deliveries",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Suppliers_SupplierId",
                table: "Receipts",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Customers_CustomerId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Suppliers_SupplierId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_SupplierId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_CustomerId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Deliveries");
        }
    }
}
