using BetterTravel.DataAccess.Cache;

namespace BetterTravel.DataAccess.Redis.Abstractions
{
    public interface ICurrencyRateRepository : ICacheRepository<CurrencyRate>
    {
    }
}