using System.Threading.Tasks;
using BetterTravel.Application.HotToursFetcher.Requests;
using BetterTravel.Application.HotToursFetcher.Responses;
using Refit;

namespace BetterTravel.Application.HotToursFetcher.Abstractions
{
    public interface IHotToursProviderApi
    {
        [Post("/toursearch/hottours")]
        Task<PoehalisnamiResponse> HotTours([Body] HotToursRequest request);
    }
}