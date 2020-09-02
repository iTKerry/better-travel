using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Application.Exchange.Abstractions;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.Queries.Abstractions;
using BetterTravel.Queries.ViewModels;
using CSharpFunctionalExtensions;

namespace BetterTravel.Queries.Exchange.GetExchange
{
    public class GetExchangeQueryHandler : QueryHandlerBase<GetExchangeQuery, List<GetExchangeViewModel>>
    {
        private readonly IExchangeProvider _provider;

        public GetExchangeQueryHandler(IExchangeProvider provider) => 
            _provider = provider;

        public override async Task<IHandlerResult<List<GetExchangeViewModel>>> Handle(
            GetExchangeQuery request,
            CancellationToken cancellationToken) =>
            await _provider.GetExchangeAsync()
                .Map(rates => rates
                    .Select(t => new GetExchangeViewModel
                    {
                        Code = t.Type.ToString(),
                        Rate = t.Rate,
                        Date = t.ExchangeDate
                    }).ToList())
                .Finally(result => result.IsFailure
                    ? ValidationFailed(result.Error)
                    : Ok(result.Value));
    }
}