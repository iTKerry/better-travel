using System;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstractions.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly WriteDbContext _dbContext;
        private readonly IDbContextTransaction _transaction;
        private readonly ILogger<UnitOfWork> _logger;
        
        private bool _disposed;

        public UnitOfWork(
            WriteDbContext dbContext,
            IChatWriteRepository chatWriteRepository, 
            IHotToursWriteRepository hotToursWriteRepository, 
            ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _transaction = dbContext.Database.BeginTransaction();
            _logger = logger;
            
            ChatWriteRepository = chatWriteRepository;
            HotToursWriteRepository = hotToursWriteRepository;
        }

        public IHotToursWriteRepository HotToursWriteRepository { get; }
        public IChatWriteRepository ChatWriteRepository { get; }
        
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