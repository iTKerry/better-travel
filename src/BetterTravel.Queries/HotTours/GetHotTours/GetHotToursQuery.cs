using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BetterTravel.DataAccess.Abstractions.Enums;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Queries.HotTours.GetHotTours
{
    public class GetHotToursQuery : IQuery<List<HotToursViewModel>>
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public int[] Countries { get; set; }
        public int[] Departures { get; set; }
        public HotelCategoryType[] HotelCategories { get; set; }
        
        public Expression<Func<HotToursViewModel, bool>> Filter => tour =>
            (!Countries.Any() || Countries.Contains(tour.CountryId)) &&
            (!Departures.Any() || Departures.Contains(tour.DepartureLocationId)) &&
            (!HotelCategories.Any() || HotelCategories.Contains(tour.HotelCategory));
    }
}