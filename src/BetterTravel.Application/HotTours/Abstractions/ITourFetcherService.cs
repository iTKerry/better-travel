using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities;

namespace BetterTravel.Application.HotTours.Abstractions
{
    public interface ITourFetcherService
    {
        Task<List<HotTour>> FetchToursAsync(HotToursRequestObject requestObject);
    }
}