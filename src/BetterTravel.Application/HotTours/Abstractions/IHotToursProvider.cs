using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities;

namespace BetterTravel.Application.HotTours.Abstractions
{
    public interface IHotToursProvider
    {
        Task<HotTour> GetHotToursAsync(HotToursQueryObject queryObject);
    }
}