using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.MediatR.Core.HandlerResults
{
    public class NotFoundHandlerResult<T> : IHandlerResult<T>
    {
    }
    
    public class NotFoundHandlerResult : IHandlerResult
    {
    }
}