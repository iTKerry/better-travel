using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.MediatR.Core
{
    public abstract class QueryHandlerBase<TRequest, TResponse> : RequestHandlerBase<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
    }
}