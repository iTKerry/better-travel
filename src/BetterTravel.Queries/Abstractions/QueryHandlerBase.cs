using BetterTravel.MediatR.Core;

namespace BetterTravel.Queries.Abstractions
{
    public abstract class QueryHandlerBase<TRequest, TResponse> : RequestHandlerBase<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
    }
}