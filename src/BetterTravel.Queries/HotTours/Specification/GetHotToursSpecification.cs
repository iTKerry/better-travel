using System;
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
                (request.CountryName == null || request.CountryName == tour.Country.Name) &&
                (request.ResortName == null || request.ResortName == tour.Resort.Name) &&
                (request.Stars == Stars.All || request.Stars == tour.Info.Stars);
    }
}