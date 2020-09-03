using BetterTravel.DataAccess.Cache.Base;

namespace BetterTravel.DataAccess.Cache
{
    public class HotTourFoundData : CachedObject
    {
        public int EntityId { get; set; }
        public string Title { get; set; }
    }
}