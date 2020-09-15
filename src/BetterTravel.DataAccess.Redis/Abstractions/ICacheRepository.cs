using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstractions.Cache.Base;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Redis.Abstractions
{
    public interface ICacheRepository<T> where T : CachedObject
    {
        string Key { get; }
        
        Task<Result<List<T>>> GetValuesAsync();
        Task<Result> SetValuesAsync(List<T> values);
    }
}