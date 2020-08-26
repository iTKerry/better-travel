using System;
using System.Net;
using System.Threading.Tasks;
using BetterTravel.Api.ExceptionHandling.Abstractions;
using Microsoft.AspNetCore.Http;
using Telegram.Bot.Exceptions;

namespace BetterTravel.Api.ExceptionHandling.ExceptionHandlers
{
    public class TelegramExceptionHandler : BaseExceptionHandler, IExceptionHandler<ApiRequestException>
    {
        protected override ErrorResponse CreateErrorMessage(Exception exception) => 
            new ErrorResponse(HttpStatusCode.OK, string.Empty);

        public Task HandleException(ApiRequestException exception, HttpContext context) => 
            base.HandleException(exception, context);
    }
}