using System;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace BetterTravel.DataAccess.EF.Common
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IDbContextTransaction _transaction;
        private readonly ILogger<UnitOfWork> _logger;
        
        private bool _disposed;

        public UnitOfWork(
            AppDbContext dbContext,
            IChatRepository chatRepository, 
            IHotToursRepository hotToursRepository, 
            ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _transaction = dbContext.Database.BeginTransaction();
            _logger = logger;
            
            ChatRepository = chatRepository;
            HotToursRepository = hotToursRepository;
        }

        public IHotToursRepository HotToursRepository { get; }
        public IChatRepository ChatRepository { get; }
        
        public async Task CommitAsync()
        {
            if (_disposed)
                throw new ObjectDisposedException(_dbContext.GetType().FullName);

            try
            {
                await _dbContext.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, e.Message);
                await RollbackAsync();
            }
            finally
            {
                Dispose();
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        private async Task Dispose(bool disposing)
        {
            if (_disposed) 
                return;

            if (disposing)
            {
                await _dbContext.DisposeAsync();
                await _transaction.DisposeAsync();
            }
            
            _disposed = true;
        }

        public async void Dispose()
        {
            try
            {
                await Dispose(true);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, e.Message);
                throw;
            }
            finally
            {
                // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
                GC.SuppressFinalize(this);
            }
        }
    }
}