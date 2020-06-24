using Autofac;
using AutoMapper;
using BetterTravel.Api.Extensions.ApplicationBuilder;
using BetterTravel.Api.Extensions.ServiceCollection;
using BetterTravel.Api.Infrastructure.HostedServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

[assembly: ApiController]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace BetterTravel.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => 
            _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) =>
            services.AddOptions()
                .AddAutoMapper(typeof(Startup).Assembly)
                .AddBetterTravelCompression()
                .AddBetterTravelMvc()
                .AddBetterTravelProfiler()
                .AddBetterTravelDb(_configuration)
                .AddMemoryCache()
                .AddBetterTravelCors()
                .AddRouteOptions()
                .AddBetterTravelHealthChecks()
                .AddBetterTravelSwagger()
                .AddHostedService<TelegramHostedService>()
                .AddHostedService<HotToursFetcherHostedService>();

        public static void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterAssemblyModules(typeof(Startup).Assembly);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app.UseExceptionHandler(env)
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseBetterTravelSwaggerUi()
                .UseSerilogRequestLogging()
                .UseCors()
                .UseAuthentication()
                .UseRouting()
                .UseEndpoints(e =>
                {
                    e.MapControllers();
                    e.MapHealthChecks("/_health");
                });
    }
}