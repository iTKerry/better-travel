using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.MediatR.Core
{
    public abstract class CommandHandlerBase<TRequest> : RequestHandlerBase<TRequest> 
        where TRequest : ICommand
    {
    }
}