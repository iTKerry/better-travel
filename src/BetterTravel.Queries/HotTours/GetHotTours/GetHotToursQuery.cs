using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BetterTravel.DataAccess.Abstractions.Enums;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Queries.HotTours.GetHotTours
{
    public sealed class GetHotToursQuery : IQuery<List<HotToursViewModel>>
    {
        public GetHotToursQuery(
            int[] countries, 
            int[] departures, 
            HotelCategoryType[] hotelCategories)
        {
            Filter = tour =>
                (!countries.Any() || countries.Contains(tour.CountryId)) &&
                (!departures.Any() || departures.Contains(tour.DepartureLocationId)) &&
                (!hotelCategories.Any() || hotelCategories.Contains(tour.HotelCategory));
        }
        
        public int Take { get; set; }
        public int Skip { get; set; }

        public Expression<Func<HotToursViewModel, bool>> Filter { get; }
    }
}