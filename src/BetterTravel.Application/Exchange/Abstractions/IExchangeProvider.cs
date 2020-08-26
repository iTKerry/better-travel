using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Cache;
using CSharpFunctionalExtensions;

namespace BetterTravel.Application.Exchange.Abstractions
{
    public interface IExchangeProvider
    {
        public Task<Result<List<CurrencyRate>>> GetExchangeAsync(bool cached = true);
    }
}