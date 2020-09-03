using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Cache;
using BetterTravel.DataAccess.Redis.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Distributed;

namespace BetterTravel.DataAccess.Redis.Repositories
{
    public class HotTourFoundRepository 
        : CacheRepository<HotTourFoundData>, IHotTourFoundRepository
    {
        public override string Key => nameof(HotTourFoundRepository);
        
        protected override DistributedCacheEntryOptions Options => new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12)
        };
        
        public HotTourFoundRepository(IDistributedCache cache) 
            : base(cache)
        {
        }

        public async Task<Result> AddValueAsync(HotTourFoundData data) =>
            await GetValuesAsync()
                .OnFailureCompensate(err => Result
                    .SuccessIf(err == "There is no cache data in store", err)
                    .Map(() => new List<HotTourFoundData>()))
                .Tap(list => list.Add(data))
                .Bind(SetValuesAsync);

        public async Task<Result> DeleteAsync()
        {
            try
            {
                await Cache.RemoveAsync(Key);
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
        }
    }
}