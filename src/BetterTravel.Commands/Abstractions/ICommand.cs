using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using MediatR;

namespace BetterTravel.Commands.Abstractions
{
    public interface ICommand : IRequest<IHandlerResult>
    {
    }
}