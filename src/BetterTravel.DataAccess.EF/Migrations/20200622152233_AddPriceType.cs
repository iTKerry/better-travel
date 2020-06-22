using Microsoft.EntityFrameworkCore.Migrations;

namespace BetterTravel.DataAccess.EF.Migrations
{
    public partial class AddPriceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "dbo",
                table: "HotTour",
                newName: "PriceAmount");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "dbo",
                table: "HotTour",
                newName: "DurationType");

            migrationBuilder.RenameColumn(
                name: "Count",
                schema: "dbo",
                table: "HotTour",
                newName: "DurationCount");

            migrationBuilder.RenameColumn(
                name: "Location",
                schema: "dbo",
                table: "HotTour",
                newName: "DepartureLocation");

            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "dbo",
                table: "HotTour",
                newName: "DepartureDate");

            migrationBuilder.AddColumn<int>(
                name: "PriceType",
                schema: "dbo",
                table: "HotTour",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceType",
                schema: "dbo",
                table: "HotTour");

            migrationBuilder.RenameColumn(
                name: "PriceAmount",
                schema: "dbo",
                table: "HotTour",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "DurationType",
                schema: "dbo",
                table: "HotTour",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "DurationCount",
                schema: "dbo",
                table: "HotTour",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "DepartureLocation",
                schema: "dbo",
                table: "HotTour",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                schema: "dbo",
                table: "HotTour",
                newName: "Date");
        }
    }
}
