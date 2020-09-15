using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.MediatR.Core.HandlerResults
{
    public class PagedDataHandlerResult<T> : IHandlerResult<T>
    {
        public PagedDataHandlerResult(T data, int count)
        {
            Data = data;
            Count = count;
        }

        public T Data { get; }
        public int Count { get; }
    }
}