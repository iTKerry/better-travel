using System.Diagnostics.CodeAnalysis;
using Autofac;
using BetterTravel.Api.ExceptionHandling;
using BetterTravel.Api.ExceptionHandling.Abstractions;
using BetterTravel.Application.Exchange;
using BetterTravel.Application.Exchange.Abstractions;
using BetterTravel.Application.HotToursFetcher;
using BetterTravel.Application.HotToursFetcher.Abstractions;
using BetterTravel.Application.Services;
using BetterTravel.Application.Services.Abstractions;
using BetterTravel.Application.Services.HotToursFetcher;
using BetterTravel.Common.Configurations;
using MediatR;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace BetterTravel.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ExchangeProvider>()
                .As<IExchangeProvider>()
                .InstancePerDependency();

            builder
                .RegisterType<HotToursProvider>()
                .As<IHotToursProvider>()
                .InstancePerDependency();

            builder
                .RegisterType<HotToursFetcherService>()
                .As<IHotToursFetcherService>()
                .AsImplementedInterfaces();

            builder
                .RegisterType<HotToursNotifierService>()
                .As<IHotToursNotifierService>()
                .AsImplementedInterfaces();
            
            builder
                .Register(context =>
                {
                    var config = context.Resolve<IOptions<BotConfiguration>>();
                    return new TelegramBotClient(config.Value.BotToken);
                }).As<ITelegramBotClient>()
                .SingleInstance();
            
            RegisterExceptionHandling(builder);
            RegisterMediator(builder);
        }

        private void RegisterExceptionHandling(ContainerBuilder builder)
        {
            builder.RegisterType<ExceptionRequestHandler>()
                .As<IExceptionRequestHandler>();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IExceptionHandler<>))
                .As(typeof(IExceptionHandler<>));
        }

        private void RegisterMediator(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}