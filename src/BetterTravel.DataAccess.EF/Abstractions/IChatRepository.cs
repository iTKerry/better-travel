using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Entities;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<List<Chat>> GetAllAsync();
    }
}