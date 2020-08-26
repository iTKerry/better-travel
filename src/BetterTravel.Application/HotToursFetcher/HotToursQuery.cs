namespace BetterTravel.Application.HotToursFetcher
{
    public class HotToursQuery
    {
        public int DurationFrom { get; set; }
        public int DurationTo { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public int Count { get; set; }
    }
}