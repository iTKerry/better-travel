using BetterTravel.Api.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BetterTravel.Api.Extensions.ApplicationBuilder
{
    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env) =>
            env.IsDevelopment()
                ? app.UseDeveloperExceptionPage()
                : app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}