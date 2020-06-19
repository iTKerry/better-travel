using BetterTravel.MediatR.Core.HandlerResults.Abstractions;

namespace BetterTravel.MediatR.Core.HandlerResults
{
    public class OkHandlerResult<T> : IHandlerResult<T>
    {
        public OkHandlerResult(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
    
    public class OkHandlerResult : IHandlerResult
    {
    }
}