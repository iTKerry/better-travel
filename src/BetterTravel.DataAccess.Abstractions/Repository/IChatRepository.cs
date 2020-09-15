using System.Collections.Generic;
using System.Threading.Tasks;
using BetterExtensions.Domain.Repository;
using BetterTravel.DataAccess.Abstractions.Entities;

namespace BetterTravel.DataAccess.Abstractions.Repository
{
    public interface IChatWriteRepository : IWriteRepository<Chat>
    {
        Task<List<Chat>> GetAllAsync();
    }
}