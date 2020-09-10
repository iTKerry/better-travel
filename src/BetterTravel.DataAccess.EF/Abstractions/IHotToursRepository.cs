using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Entities;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public interface IHotToursRepository : IRepository<HotTour>
    {
        Task<List<HotTour>> GetAllAsync();
    }
}