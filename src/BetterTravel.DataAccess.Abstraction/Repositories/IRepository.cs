using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstraction.Entities.Base;
using CSharpFunctionalExtensions;

namespace BetterTravel.DataAccess.Abstraction.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<List<T>> GetAsync(QueryObject<T> queryObject);
        Task<List<TResult>> GetAsync<TResult>(QueryObject<T, TResult> queryObject);

        Task<Maybe<T>> GetByIdAsync(int id);
        Task<Maybe<T>> GetFirstAsync(Expression<Func<T, bool>> wherePredicate);

        void Save(T chat);
        Task SaveAsync(List<T> chats);
        
        Task DeleteByIdAsync(int id);
        void Delete(T entity);
    }
}