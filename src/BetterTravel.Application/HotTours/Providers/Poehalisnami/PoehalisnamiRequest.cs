namespace BetterTravel.Application.HotTours.Providers.Poehalisnami
{
    public class PoehalisnamiRequest
    {
        public string CountryIds { get; set; }
        public string ResortIds { get; set; }
        public string DepartureIds { get; set; }
        public string HotelCategoryIds { get; set; }
        public string BoardIds { get; set; }
        public string RestTypeIds { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public long DurationQtyDayFrom { get; set; }
        public long DurationQtyDayTo { get; set; }
        public long PriceFrom { get; set; }
        public long PriceTo { get; set; }
        public long LanguageId { get; set; }
        public long CurrentPage { get; set; }
        public long PageSize { get; set; }
        public long SortId { get; set; }
    }
}