using System;
using System.Threading.Tasks;

namespace BetterTravel.DataAccess.Abstraction.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IHotToursRepository HotToursRepository { get; }
        IChatRepository ChatRepository { get; }

        Task CommitAsync();
    }
}