using System.Threading.Tasks;
using BetterTravel.Application.HotTours.Providers.Poehalisnami;
using BetterTravel.Application.HotTours.Providers.Poehalisnami.Responses;
using Refit;

namespace BetterTravel.Application.HotTours.Abstractions
{
    public interface IPoehalisnamiApi
    {
        [Post("/toursearch/hottours")]
        Task<PoehalisnamiResponse> HotTours([Body] PoehalisnamiRequest request);
    }
}