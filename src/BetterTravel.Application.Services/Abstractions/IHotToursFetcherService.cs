using System.Threading.Tasks;
using BetterTravel.Application.HotToursFetcher;

namespace BetterTravel.Application.Services.Abstractions
{
    public interface IHotToursFetcherService
    {
        Task FetchAndStore(HotToursQuery toursQuery);
    }
}