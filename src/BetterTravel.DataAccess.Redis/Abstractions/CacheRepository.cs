using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Cache.Base;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BetterTravel.DataAccess.Redis.Abstractions
{
    public abstract class CacheRepository<T> : ICacheRepository<T>
        where T : CachedObject
    {
        protected readonly IDistributedCache Cache;

        public abstract string Key { get; }
        protected abstract DistributedCacheEntryOptions Options { get; }

        protected CacheRepository(IDistributedCache cache) => 
            Cache = cache;

        public virtual async Task<Result<List<T>>> GetValuesAsync() => 
            await GetAsync<List<T>>();

        public virtual async Task<Result> SetValuesAsync(List<T> values) => 
            await SetAsync(values);

        private async Task<Result<TData>> GetAsync<TData>()
        {
            try
            {
                return ((Maybe<byte[]>) await Cache.GetAsync(Key))
                    .ToResult("There is no cache data in store")
                    .Map(bytes => Encoding.UTF8.GetString(bytes))
                    .Map(JsonConvert.DeserializeObject<TData>);
            }
            catch (Exception e)
            {
                return Result.Failure<TData>($"Critical error while trying to get key: {Key}. Message: {e.Message}");
            }
        }
        
        private async Task<Result> SetAsync<TData>(TData data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data);
                var bytes = Encoding.UTF8.GetBytes(json);
                await Cache.SetAsync(Key, bytes, Options);
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure($"Critical error while trying to store key: {Key}. Message: {e.Message}");
            }
        }
    }
}