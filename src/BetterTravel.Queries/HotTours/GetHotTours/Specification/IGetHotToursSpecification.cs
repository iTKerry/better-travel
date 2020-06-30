using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Specification;

namespace BetterTravel.Queries.HotTours.GetHotTours.Specification
{
    public interface IGetHotToursSpecification 
        : ISpecification<HotTour, GetHotToursQuery>
    {
    }
}