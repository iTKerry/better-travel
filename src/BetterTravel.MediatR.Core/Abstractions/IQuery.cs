using MediatR;

namespace BetterTravel.MediatR.Core.Abstractions
{
    public interface IQuery<T> : IRequest<IHandlerResult<T>>
    {
    }
}