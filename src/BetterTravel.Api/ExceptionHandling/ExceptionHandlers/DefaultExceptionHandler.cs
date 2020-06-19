using System;
using System.Net;
using BetterTravel.Api.ExceptionHandling.Abstractions;

namespace BetterTravel.Api.ExceptionHandling.ExceptionHandlers
{
    public class DefaultExceptionHandler : BaseExceptionHandler, IExceptionHandler<Exception>
    {
        private const string ErrorMessage = "Some unexpected error occurred.";

        protected override ErrorResponse CreateErrorMessage(Exception exception) =>
            new ErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage);
    }
}