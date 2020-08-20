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
                        C.CountryID,
                        DL.DepartureLocationID,
                        HC.HotelCategoryID,
                        C2.CurrencyId
                    FROM HotTour
                        JOIN Country C on C.CountryID = HotTour.CountryId
                        JOIN DepartureLocation DL on HotTour.DepartureLocationId = DL.DepartureLocationID
                        JOIN HotelCategory HC on HotTour.CategoryId = HC.HotelCategoryID
                        JOIN Currency C2 on HotTour.Price_CurrencyId = C2.CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW HotTourView");
        }
    }
}
