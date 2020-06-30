using System;
using System.Linq;
using System.Linq.Expressions;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Specification;

namespace BetterTravel.Queries.HotTours.GetHotTours.Specification
{
    public class GetHotToursSpecification : 
        SpecificationBase<HotTour, GetHotToursQuery>,
        IGetHotToursSpecification
    {
        public override Expression<Func<HotTour, bool>> ToExpression(GetHotToursQuery request) =>
            tour =>
                (!request.Countries.Any() || request.Countries.Contains(tour.Country.Id)) &&
                (!request.Departures.Any() || request.Departures.Contains(tour.DepartureLocation.Id)) &&
                (!request.HotelCategories.Any() || request.HotelCategories.Contains(tour.Category.Id));
    }
}