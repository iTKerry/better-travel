using System;

namespace BetterTravel.Application.HotToursFetcher.Requests
{
    public class HotToursRequest
    {
        public HotToursRequest(HotToursQuery query)
        {
            CountryIds = string.Empty;
            ResortIds = string.Empty;
            DepartureIds = string.Empty;
            HotelCategoryIds = string.Empty;
            BoardIds = string.Empty;
            RestTypeIds = string.Empty;
            DateFrom = DateTime.Now.ToString("dd-MM-yyyy");
            DateTo = DateTime.Now.AddYears(1).ToString("dd-MM-yyyy");
            DurationQtyDayFrom = query.DurationFrom;
            DurationQtyDayTo = query.DurationTo;
            PriceFrom = query.PriceFrom;
            PriceTo = query.PriceTo;
            LanguageId = 2;
            CurrentPage = 1;
            PageSize = query.Count;
            SortId = 0;
        }
        
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