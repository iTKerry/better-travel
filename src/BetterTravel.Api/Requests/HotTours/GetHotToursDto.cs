using BetterTravel.DataAccess.Abstraction.Entities.Enums;

namespace BetterTravel.Api.Requests.HotTours
{
    public class GetHotToursDto
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public string CountryName { get; set; }
        public string ResortName { get; set; }
        public Stars Stars { get; set; } = Stars.All;
    }
}