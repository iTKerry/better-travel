using System.Threading.Tasks;
using BetterTravel.DataAccess.Cache;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Redis.Abstractions
{
    public interface IHotTourFoundRepository 
        : ICacheRepository<HotTourFoundData>
    {
        Task<Result> AddValueAsync(HotTourFoundData data);
        Task<Result> DeleteAsync();
    }
}