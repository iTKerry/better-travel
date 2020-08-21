using System.Collections.Generic;
using BetterTravel.DataAccess.Redis.Base;
using BetterTravel.Domain.Cache;
using Microsoft.Extensions.Caching.Distributed;

namespace BetterTravel.DataAccess.Redis
{
    public class CurrencyRateCacheProvider : CacheProvider<List<CurrencyRate>>
    {
        public CurrencyRateCacheProvider(IDistributedCache cache) : base(cache)
        {
        }
    }
}