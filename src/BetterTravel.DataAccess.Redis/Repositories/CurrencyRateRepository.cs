using System;
using BetterTravel.DataAccess.Abstractions.Cache;
using BetterTravel.DataAccess.Redis.Abstractions;
using Microsoft.Extensions.Caching.Distributed;

namespace BetterTravel.DataAccess.Redis.Repositories
{
    public class CurrencyRateRepository : CacheRepository<CurrencyRate>, ICurrencyRateRepository
    {
        public override string Key => nameof(CurrencyRateRepository);
        
        protected override DistributedCacheEntryOptions Options => new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3)
        };

        public CurrencyRateRepository(IDistributedCache cache) 
            : base(cache)
        {
        }
    }
}