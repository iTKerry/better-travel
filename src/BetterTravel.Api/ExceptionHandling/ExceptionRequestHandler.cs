using System;
using System.Threading.Tasks;
using BetterTravel.Api.ExceptionHandling.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BetterTravel.Api.ExceptionHandling
{
    public class ExceptionRequestHandler : IExceptionRequestHandler
    {
        private static readonly Type HandlerOpenType = typeof(IExceptionHandler<>);

        private readonly ILogger<IExceptionHandler> _logger;

        public ExceptionRequestHandler(ILogger<IExceptionHandler> logger) =>
            _logger = logger;

        public async Task Handle(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            Type[] typeArgs = {exception.GetType()};

            var handler = context.RequestServices?.GetService(HandlerOpenType.MakeGenericType(typeArgs))
                          ?? context.RequestServices?.GetService<IExceptionHandler<Exception>>();

            if (handler == null)
            {
                _logger.LogError("Can't resolve exception handler for {type}", exception.GetType());
                return;
            }

            await ((IExceptionHandler) handler).HandleException(exception, context);
        }
    }
}