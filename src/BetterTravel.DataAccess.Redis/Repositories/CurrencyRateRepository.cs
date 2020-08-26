using BetterTravel.DataAccess.Cache;
using BetterTravel.DataAccess.Redis.Abstractions;
using Microsoft.Extensions.Caching.Distributed;

namespace BetterTravel.DataAccess.Redis.Repositories
{
    public class CurrencyRateRepository : CacheRepository<CurrencyRate>, ICurrencyRateRepository
    {
        public CurrencyRateRepository(IDistributedCache cache) 
            : base(cache)
        {
        }
    }
}