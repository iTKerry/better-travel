using System;
using System.Threading.Tasks;
using BetterTravel.Api.ExceptionHandling.Abstractions;
using Microsoft.AspNetCore.Http;

namespace BetterTravel.Api.Infrastructure.Middlewares
{
    public sealed class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionRequestHandler _exceptionRequestHandler;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            IExceptionRequestHandler exceptionRequestHandler)
        {
            _exceptionRequestHandler = exceptionRequestHandler;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) when (!(ex is StackOverflowException))
            {
                await _exceptionRequestHandler.Handle(httpContext, ex);
            }
        }
    }
}