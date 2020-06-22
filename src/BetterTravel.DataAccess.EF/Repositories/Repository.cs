using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities.Base;
using BetterTravel.DataAccess.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public abstract class Repository<T> : IRepository<T> 
        where T : Entity
    {
        private readonly AppDbContext _db;

        public Repository(AppDbContext db) => 
            _db = db;

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> wherePredicate) =>
            await _db.Set<T>()
                .Where(wherePredicate)
                .ToListAsync();

        public async Task<List<TResult>> GetAllAsync<TResult>(
            Expression<Func<T, bool>> wherePredicate, 
            Expression<Func<T, TResult>> projection) =>
            await _db.Set<T>()
                .Where(wherePredicate)
                .Select(projection)
                .ToListAsync();
    }
}