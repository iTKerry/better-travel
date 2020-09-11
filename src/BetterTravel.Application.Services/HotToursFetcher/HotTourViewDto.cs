using System;

namespace BetterTravel.Application.Services.HotToursFetcher
{
    internal class HotTourViewDto
    {
        public string Name { get; set; }
        public int PriceAmount { get; set; }
        public DateTime DepartureDate { get; set; }
        public int DepartureLocationId { get; set; }
    }
}