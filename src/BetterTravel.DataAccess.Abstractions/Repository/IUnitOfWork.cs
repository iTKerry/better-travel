using System;
using System.Threading.Tasks;

namespace BetterTravel.DataAccess.Abstractions.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IHotToursWriteRepository HotToursWriteRepository { get; }
        IChatWriteRepository ChatWriteRepository { get; }

        Task CommitAsync();
        Task RollbackAsync();
    }
}