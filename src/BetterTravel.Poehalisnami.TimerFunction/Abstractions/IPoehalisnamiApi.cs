using System.Threading.Tasks;
using BetterTravel.Poehalisnami.TimerFunction.Requests;
using BetterTravel.Poehalisnami.TimerFunction.Responses;
using Refit;

namespace BetterTravel.Poehalisnami.TimerFunction.Abstractions
{
    public interface IPoehalisnamiApi
    {
        [Post("/toursearch/hottours")]
        Task<PoehalisnamiResponse> HotTours([Body] PoehalisnamiRequest request);
    }
}