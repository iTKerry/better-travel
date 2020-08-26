using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using BetterTravel.DataAccess.EF;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.EF.Common;
using BetterTravel.DataAccess.EF.Repositories;
using BetterTravel.DataAccess.EF.Seeder;
using BetterTravel.DataAccess.EF.Seeder.Abstractions;
using BetterTravel.DataAccess.Redis.Abstractions;
using BetterTravel.DataAccess.Redis.Repositories;
using Module = Autofac.Module;

namespace BetterTravel.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class DataAccessModule : Module
    {
        protected override Assembly ThisAssembly => typeof(AppDbContext).Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DbSeeder>()
                .As<IDbSeeder>();
            
            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>();
            
            builder
                .RegisterType<EventDispatcher>()
                .As<IEventDispatcher>();
            
            builder
                .RegisterType<Bus>()
                .As<IBus>();
            
            builder
                .RegisterType<MessageBus>()
                .AsSelf();
            
            builder
                .RegisterType<HotToursRepository>()
                .As<IHotToursRepository>();

            builder
                .RegisterType<ChatRepository>()
                .As<IChatRepository>();

            builder
                .RegisterGeneric(typeof(ReadOnlyRepository<>))
                .As(typeof(IReadOnlyRepository<>))
                .InstancePerDependency();

            builder
                .RegisterType<CurrencyRateRepository>()
                .As<ICurrencyRateRepository>();
        }
    }
}