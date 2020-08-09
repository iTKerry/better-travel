using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.Domain.Entities;

namespace BetterTravel.Application.HotTours.Abstractions
{
    public interface IHotToursProvider
    {
        Task<List<HotTour>> GetHotToursAsync(HotToursRequestObject requestObject);
    }
}