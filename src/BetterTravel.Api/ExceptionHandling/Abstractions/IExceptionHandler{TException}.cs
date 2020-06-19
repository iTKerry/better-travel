using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BetterTravel.Api.ExceptionHandling.Abstractions
{
    public interface IExceptionHandler<in TException> : IExceptionHandler
        where TException : Exception
    {
        Task HandleException(TException exception, HttpContext context);
    }
}