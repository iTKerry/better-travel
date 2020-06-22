using BetterTravel.DataAccess.Abstraction.Entities.Enums;

namespace BetterTravel.Api.Requests.HotTours
{
    public class GetHotToursDto
    {
        public int Take { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public string Country { get; set; } = string.Empty;
        public string Resort { get; set; } = string.Empty;
        public Stars Stars { get; set; } = Stars.Unknown;
    }
}