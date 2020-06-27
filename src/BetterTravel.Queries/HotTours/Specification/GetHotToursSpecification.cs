using System;
using System.Linq;
using System.Linq.Expressions;
using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.Abstraction.Entities.Enums;
using BetterTravel.DataAccess.Abstraction.Specification;

namespace BetterTravel.Queries.HotTours.Specification
{
    public class GetHotToursSpecification : 
        SpecificationBase<HotTour, GetHotToursQuery>,
        IGetHotToursSpecification
    {
        public override Expression<Func<HotTour, bool>> ToExpression(GetHotToursQuery request) =>
            tour =>
                (!request.Countries.Any() || request.Countries.Contains(tour.Country.Id)) &&
                (!request.Departures.Any() || request.Departures.Contains(tour.DepartureLocation.Id)) &&
                (request.Stars == Stars.All || request.Stars == tour.Info.Stars);
    }
}