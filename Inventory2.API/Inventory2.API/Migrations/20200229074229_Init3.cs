using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory2.API.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Inventories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_UnitId",
                table: "Inventories",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Units_UnitId",
                table: "Inventories",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Units_UnitId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_UnitId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Inventories");
        }
    }
}
