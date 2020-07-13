using Microsoft.EntityFrameworkCore.Migrations;

namespace BetterTravel.DataAccess.EF.Migrations
{
    public partial class HotTourView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW HotTourView
                AS
                    SELECT
                        HotTour.Name,
                        ResortName,
                        DepartureDate,
                        ImageLink,
                        DetailsLink,
                        DurationCount,
                        DurationType,
                        PriceAmount,
                        PriceType,
                        C.Name as CountryName,
                        DL.Name as DepartureName,
                        HC.Name as HotelCategory
                    FROM HotTour
                    JOIN Country C on C.CountryID = HotTour.CountryId
                    JOIN DepartureLocation DL on HotTour.DepartureLocationId = DL.DepartureLocationID
                    JOIN HotelCategory HC on HotTour.CategoryId = HC.HotelCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW HotTourView");
        }
    }
}
