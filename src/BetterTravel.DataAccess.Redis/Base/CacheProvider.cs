using System;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BetterTravel.DataAccess.Redis.Base
{
    public abstract class CacheProvider<TCache>
    {
        protected readonly IDistributedCache Cache;

        protected CacheProvider(IDistributedCache cache) => 
            Cache = cache;

        public virtual async Task<Result<TCache>> GetValueAsync(string key)
        {
            try
            {
                return ((Maybe<byte[]>) await Cache.GetAsync(key))
                    .ToResult("There is no cache data in store")
                    .Map(bytes => Encoding.UTF8.GetString(bytes))
                    .Map(JsonConvert.DeserializeObject<TCache>);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result.Failure<TCache>($"Critical error while trying to get key: {key}. Message: {e.Message}");
            }
        }

        public virtual async Task<Result> SetValueAsync(string key, TCache value, TimeSpan expirationTime)
        {
            try
            {
                var json = JsonSerializer.Serialize(value);
                var bytes = Encoding.UTF8.GetBytes(json);
                await Cache.SetAsync(key, bytes);
                return Result.Success();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result.Failure($"Critical error while trying to store key: {key}. Message: {e.Message}");
            }
        }
    }
}