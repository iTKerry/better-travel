using System;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Repositories;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
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
            await Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async Task Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                await _db.DisposeAsync();

            _disposed = true;
        }
    }
}