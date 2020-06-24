using System.Diagnostics.CodeAnalysis;
using Autofac;
using BetterTravel.Api.ExceptionHandling;
using BetterTravel.Api.ExceptionHandling.Abstractions;
using BetterTravel.Api.Extensions.Configuration;
using BetterTravel.Application.HotTours;
using BetterTravel.Application.HotTours.Abstractions;
using BetterTravel.Application.HotTours.Providers.Poehalisnami;
using BetterTravel.Common.Configurations;
using MediatR;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace BetterTravel.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterTelegram(builder);
            RegisterExceptionHandling(builder);
            RegisterMediator(builder);

            builder
                .RegisterType<TourFetcherService>()
                .As<ITourFetcherService>();

            builder
                .RegisterType<PoehalisnamiProvider>()
                .As<IHotToursProvider>();
        }

        private static void RegisterTelegram(ContainerBuilder builder)
        {
            builder
                .Register<BotConfiguration>(context =>
                {
                    var c = context.Resolve<IConfiguration>();
                    return c.GetOptions<BotConfiguration>(nameof(BotConfiguration));
                }).SingleInstance();

            builder
                .Register(context =>
                {
                    var config = context.Resolve<BotConfiguration>();
                    return new TelegramBotClient(config.BotToken);
                }).As<ITelegramBotClient>()
                .SingleInstance();
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