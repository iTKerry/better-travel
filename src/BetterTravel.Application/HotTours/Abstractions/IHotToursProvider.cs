using System.Threading.Tasks;
using BetterTravel.Domain;

namespace BetterTravel.Application.HotTours.Abstractions
{
    public interface IHotToursProvider
    {
        Task<HotTour> GetHotToursAsync(HotToursQueryObject queryObject);
    }
}