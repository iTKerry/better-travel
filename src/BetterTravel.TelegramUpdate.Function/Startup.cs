using System;
using System.Reflection;
using AutoMapper;
using BetterTravel.Common.Configurations;
using BetterTravel.Common.Constants;
using BetterTravel.DataAccess.EF;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.EF.Repositories;
using BetterTravel.TelegramUpdate.Function;
using BetterTravel.TelegramUpdate.Function.Commands.Abstractions;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BetterTravel.TelegramUpdate.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddSingleton(GetDbConnectionString())
                .AddAutoMapper(typeof(Startup).Assembly)
                .AddTransient<IMediator, Mediator>()
                .AddTransient<ServiceFactory>(provider => provider.GetService)
                .AddTransient<ITelegramBotClient>(GetTelegramBotClient)
                .AddDbContext<AppDbContext>()
                .AddTransient<IHotToursRepository, HotToursRepository>()
                .AddTransient<IChatRepository, ChatRepository>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IEventDispatcher, EventDispatcher>()
                .AddTransient<MessageBus, MessageBus>()
                .AddTransient<IBus, Bus>()
                .Scan(scan => scan
                    .FromAssemblies(typeof(TelegramCommandHandler<>).GetTypeInfo().Assembly)
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
        }

        private static DbConnectionString GetDbConnectionString() =>
            new DbConnectionString(GetEnvironmentVariable(ConnectionStrings.BetterTravelDb));

        private static TelegramBotClient GetTelegramBotClient(IServiceProvider provider) => 
            new TelegramBotClient(GetEnvironmentVariable("BotToken"));

        public static string GetEnvironmentVariable(string name) =>
            Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
    }
}