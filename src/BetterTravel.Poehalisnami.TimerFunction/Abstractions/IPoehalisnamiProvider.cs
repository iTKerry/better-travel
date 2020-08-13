using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.Domain.Entities;
using BetterTravel.Poehalisnami.TimerFunction.Requests;

namespace BetterTravel.Poehalisnami.TimerFunction.Abstractions
{
    public interface IPoehalisnamiProvider
    {
        Task<List<HotTour>> GetHotToursAsync(PoehalisnamiQuery requestObject);
    }
}