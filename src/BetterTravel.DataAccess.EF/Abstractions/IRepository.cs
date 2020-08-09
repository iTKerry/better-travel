using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterTravel.DataAccess.EF.Repositories;
using BetterTravel.Domain.Entities.Base;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.EF.Abstractions
{
    public interface IRepository<TRoot> 
        where TRoot : AggregateRoot
    {
        Task<List<TRoot>> GetAsync(QueryObject<TRoot> queryObject);
        Task<List<TResult>> GetAsync<TResult>(QueryObject<TRoot, TResult> queryObject);

        Task<Maybe<TRoot>> GetByIdAsync(int id);
        Task<Maybe<TRoot>> GetFirstAsync(Expression<Func<TRoot, bool>> wherePredicate);

        void Save(TRoot chat);
        void Save(List<TRoot> chats);
        
        Task DeleteByIdAsync(int id);
        void Delete(TRoot entity);
    }
}