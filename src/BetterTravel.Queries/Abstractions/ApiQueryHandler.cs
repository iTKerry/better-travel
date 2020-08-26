using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Queries.Abstractions
{
    public abstract class ApiQueryHandler<TRequest, TResponse> 
        : QueryHandlerBase<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
    }
}