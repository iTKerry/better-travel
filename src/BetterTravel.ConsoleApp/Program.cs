using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace BetterTravel.ConsoleApp
{
    internal class Program
    {
        private static async Task Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var api = RestService.For<IPoehalisnamiApi>("https://www.poehalisnami.ua");
            var query = new HotToursQuery
            {
                CountryIds = string.Empty,
                ResortIds = string.Empty,
                DepartureIds = string.Empty,
                HotelCategoryIds = string.Empty,
                BoardIds = string.Empty,
                RestTypeIds = string.Empty,
                DateFrom = DateTime.Now.ToString("dd-MM-yyyy"),
                DateTo = DateTime.Now.AddYears(1).ToString("dd-MM-yyyy"),
                DurationQtyDayFrom = 1,
                DurationQtyDayTo = 21,
                PriceFrom = 1000,
                PriceTo = 50000,
                LanguageId = 2,
                CurrentPage = 1,
                PageSize = 50,
                SortId = 0
            };
            var response = await api.HotTours(query);
            var tours = response.TourListItems
                .Select(tour => $"Name: {tour.TourName}\n" +
                                $"Stars: {tour.StarsCount.Count}\n" +
                                $"Price: {tour.PriceFromString} {tour.PriceDescription}\n" +
                                $"Days: {tour.DurationDay} {tour.NightsText}\n" +
                                $"Order: {tour.TourDetailsUrl}\n" +
                                $"Country: {tour.CountryLinks?.Links?.FirstOrDefault()?.Text}\n" +
                                $"Resort: {tour.ResortLinks?.Links?.FirstOrDefault()?.Text}\n")
                .ToList();

            tours.ForEach(Console.WriteLine);
        }
    }

    public interface IPoehalisnamiApi
    {
        [Post("/toursearch/hottours")]
        Task<HotToursResponse> HotTours([Body] HotToursQuery hotToursQuery);
    }

    public class HotToursQuery
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

    public class HotToursResponse
    {
        public List<TourListItem> TourListItems { get; set; }
    }

    public class TourListItem
    {
        public object CountryIds { get; set; }
        public List<long> StarsCount { get; set; }
        public string DepartureDate { get; set; }
        public long DurationDay { get; set; }
        public long PriceFrom { get; set; }
        public string PriceFromString { get; set; }
        public string PriceDescription { get; set; }
        public string TourName { get; set; }
        public string DeparturePointNameGenitive { get; set; }
        public string Date { get; set; }
        public string NightsText { get; set; }
        public Uri ImageUrl { get; set; }
        public Uri TourDetailsUrl { get; set; }
        public Uri TourOrderUrl { get; set; }
        public LinksResponse CountryLinks { get; set; }
        public LinksResponse ResortLinks { get; set; }
        public string ButtonText { get; set; }
    }

    public class LinksResponse
    {
        public bool OpenInNewWindow { get; set; }
        public List<Link> Links { get; set; }
        public string ListDelimiter { get; set; }
    }

    public class Link
    {
        public Uri Href { get; set; }
        public string Text { get; set; }
        public object CssClass { get; set; }
    }
}
