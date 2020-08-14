using MediatR;

namespace BetterTravel.MediatR.Core.Abstractions
{
    public interface ICommand : IRequest<IHandlerResult>
    {
    }
}