using BetterTravel.MediatR.Core;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Commands.Abstractions
{
    public abstract class CommandHandlerBase<TRequest> : RequestHandlerBase<TRequest> 
        where TRequest : ICommand
    {
    }
}