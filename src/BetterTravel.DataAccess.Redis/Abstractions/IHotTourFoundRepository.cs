using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstractions.Cache;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Redis.Abstractions
{
    public interface IHotTourFoundRepository 
        : ICacheRepository<HotTourFoundData>
    {
        Task<Result> AddValueAsync(HotTourFoundData data);
        Task<Result> CleanAsync();
    }
}