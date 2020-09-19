using System.Collections.Generic;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstractions.Cache;
using CSharpFunctionalExtensions;

namespace BetterTravel.Application.Exchange.Abstractions
{
    public interface IExchangeProvider
    {
        Task<Result<List<CurrencyRate>>> GetExchangeAsync(bool cached = true);
    }
}