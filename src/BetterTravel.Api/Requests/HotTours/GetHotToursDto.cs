namespace BetterTravel.Api.Requests.HotTours
{
    public class GetHotToursDto
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public int[] Countries { get; set; }
        public int[] Departures { get; set; }
        public int[] HotelCategories { get; set; }
        public bool Localize { get; set; }
    }
}