using BetterTravel.DataAccess.Abstractions.Cache.Base;

namespace BetterTravel.DataAccess.Abstractions.Cache
{
    public class HotTourFoundData : CachedObject
    {
        public int EntityId { get; set; }
        public string Name { get; set; }
    }
}