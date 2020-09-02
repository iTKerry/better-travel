using System.Collections.Generic;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.Exchange.GetExchange
{
    public class GetExchangeQuery : IQuery<List<GetExchangeViewModel>>
    {
    }
}