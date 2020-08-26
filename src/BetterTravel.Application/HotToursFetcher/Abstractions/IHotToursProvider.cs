using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Entities;

namespace BetterTravel.Application.HotToursFetcher.Abstractions
{
    public interface IHotToursProvider
    {
        Task<List<HotTour>> GetHotToursAsync(HotToursQuery query);
    }
}