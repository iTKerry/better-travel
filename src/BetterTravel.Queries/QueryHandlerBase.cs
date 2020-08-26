using BetterTravel.MediatR.Core;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Queries
{
    public abstract class QueryHandlerBase<TRequest, TResponse> : RequestHandlerBase<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
    }
}