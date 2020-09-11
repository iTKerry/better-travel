using System.Threading.Tasks;

namespace BetterTravel.Application.Services.Abstractions
{
    public interface IHotToursFetcherService
    {
        Task FetchAndStore();
    }
}