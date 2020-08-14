using System.Threading.Tasks;
using BetterTravel.HotToursFetcher.Function.Requests;
using BetterTravel.HotToursFetcher.Function.Responses;
using Refit;

namespace BetterTravel.HotToursFetcher.Function.Abstractions
{
    public interface IPoehalisnamiApi
    {
        [Post("/toursearch/hottours")]
        Task<PoehalisnamiResponse> HotTours([Body] PoehalisnamiRequest request);
    }
}