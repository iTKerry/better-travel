using System.Threading.Tasks;
using BetterTravel.Application.HotTours;
using BetterTravel.Application.HotTours.Providers.Poehalisnami;

namespace BetterTravel.ConsoleApp
{
    internal class Program
    {
        private static async Task Main()
        {
            var provider = new PoehalisnamiProvider();
            var query = new HotToursQueryObject
            {
                DurationFrom = 1,
                DurationTo = 21,
                PriceFrom = 1,
                PriceTo = 10000,
                Count = 10
            };
            var result = await provider.GetHotToursAsync(query);
        }
    }
}
