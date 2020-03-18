using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory2.API.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "Inventories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_StockId",
                table: "Inventories",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Stocks_StockId",
                table: "Inventories",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Stocks_StockId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_StockId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "Inventories");
        }
    }
}
