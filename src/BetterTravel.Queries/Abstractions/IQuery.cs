using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using MediatR;

namespace BetterTravel.Queries.Abstractions
{
    public interface IQuery<T> : IRequest<IHandlerResult<T>>
    {
    }
}