using BetterTravel.MediatR.Core;

namespace BetterTravel.Commands.Abstractions
{
    public abstract class CommandHandlerBase<TRequest> : RequestHandlerBase<TRequest> 
        where TRequest : ICommand
    {
    }
}