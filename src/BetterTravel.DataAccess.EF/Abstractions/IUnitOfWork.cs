using System;
using System.Threading.Tasks;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IHotToursRepository HotToursRepository { get; }
        IChatRepository ChatRepository { get; }

        Task<int> CommitAsync();
    }
}