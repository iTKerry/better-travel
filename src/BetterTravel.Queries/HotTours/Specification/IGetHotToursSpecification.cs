using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.DataAccess.Abstraction.Specification;

namespace BetterTravel.Queries.HotTours.Specification
{
    public interface IGetHotToursSpecification 
        : ISpecification<HotTour, GetHotToursQuery>
    {
    }
}