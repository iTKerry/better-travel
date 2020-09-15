using BetterTravel.DataAccess.Abstractions.Cache;

namespace BetterTravel.DataAccess.Redis.Abstractions
{
    public interface ICurrencyRateRepository : ICacheRepository<CurrencyRate>
    {
    }
}