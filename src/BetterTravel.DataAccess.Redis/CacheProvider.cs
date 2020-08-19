using System;
using System.Text.Json;
using System.Threading.Tasks;
using BetterTravel.Domain.Cache.Base;
using CSharpFunctionalExtensions;
using StackExchange.Redis;

namespace BetterTravel.DataAccess.Redis
{
    public abstract class CacheProvider<TCache>
    {
        protected readonly IDatabase RedisDb;

        protected CacheProvider(IDatabase redisDb) => 
            RedisDb = redisDb;

        public virtual async Task<Result<TCache>> GetValueAsync(string key)
        {
            try
            {
                var redisValue = await RedisDb.StringGetAsync(key);
                return Result
                    .FailureIf(
                        !(redisValue.Box() is TCache || redisValue.Box() is null),
                        $"Invalid cast type from RedisValue to {nameof(TCache)}")
                    .Bind(() => !(redisValue.Box() is TCache result)
                        ? Result.Failure<TCache>($"Cache value is null for key: {key}")
                        : Result.Success(result));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result.Failure<TCache>(e.Message);
            }
        }

        public virtual async Task<Result> SetValueAsync(string key, TCache value, TimeSpan expirationTime)
        {
            try
            {
                var json = JsonSerializer.Serialize(value);
                var redisKey = new RedisKey(key);
                var redisValue = new RedisValue(json);

                return await RedisDb.StringSetAsync(redisKey, redisValue, expirationTime)
                    ? Result.Success()
                    : Result.Failure($"There was an error while trying to store key: {key}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result.Failure($"Critical error while trying to store key: {key}");
            }
        }
    }
}