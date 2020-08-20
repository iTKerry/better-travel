using Microsoft.EntityFrameworkCore.Migrations;

namespace BetterTravel.DataAccess.EF.Migrations
{
    public partial class PriceCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price_CurrencyId",
                schema: "dbo",
                table: "HotTour",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotTour_Price_CurrencyId",
                schema: "dbo",
                table: "HotTour",
                column: "Price_CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotTour_Currency_Price_CurrencyId",
                schema: "dbo",
                table: "HotTour",
                column: "Price_CurrencyId",
                principalSchema: "dbo",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotTour_Currency_Price_CurrencyId",
                schema: "dbo",
                table: "HotTour");

            migrationBuilder.DropIndex(
                name: "IX_HotTour_Price_CurrencyId",
                schema: "dbo",
                table: "HotTour");

            migrationBuilder.DropColumn(
                name: "Price_CurrencyId",
                schema: "dbo",
                table: "HotTour");
        }
    }
}
