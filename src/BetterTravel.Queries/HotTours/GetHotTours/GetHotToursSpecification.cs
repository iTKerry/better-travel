using System;
using System.Linq;
using System.Linq.Expressions;
using BetterTravel.Common.Specification;
using BetterTravel.DataAccess.Entities;

namespace BetterTravel.Queries.HotTours.GetHotTours
{
    public sealed class GetHotToursSpecification : Specification<HotTour, GetHotToursQuery>
    {
        public override Expression<Func<HotTour, bool>> ToExpression(GetHotToursQuery request) =>
            tour =>
                (!request.Countries.Any() || request.Countries.Contains(tour.Country.Id)) &&
                (!request.Departures.Any() || request.Departures.Contains(tour.DepartureLocation.Id)) &&
                (!request.HotelCategories.Any() || request.HotelCategories.Contains(tour.Category.Id));
    }
}