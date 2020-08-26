using System;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Abstractions;

namespace BetterTravel.DataAccess.EF.Common
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        
        private bool _disposed;

        public UnitOfWork(
            AppDbContext db,
            IChatRepository chatRepository, 
            IHotToursRepository hotToursRepository)
        {
            _db = db;
            ChatRepository = chatRepository;
            HotToursRepository = hotToursRepository;
        }

        public IHotToursRepository HotToursRepository { get; }
        public IChatRepository ChatRepository { get; }
        
        public async Task<int> CommitAsync() => 
            _disposed 
                ? throw new ObjectDisposedException(_db.GetType().FullName) 
                : await _db.SaveChangesAsync();

        public async void Dispose()
        {
            if (!_disposed)
                await _db.DisposeAsync();
            
            _disposed = true;
            
            GC.SuppressFinalize(this);
        }
    }
}