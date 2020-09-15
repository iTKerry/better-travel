using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using BetterExtensions.Domain.Repository;
using BetterTravel.DataAccess.Abstractions.Repository;
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
        protected override Assembly ThisAssembly => typeof(WriteDbContext).Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DbSeeder>()
                .As<IDbSeeder>();
            
            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
            
            builder
                .RegisterType<EventDispatcher>()
                .As<IEventDispatcher>();

            builder
                .RegisterType<HotToursWriteRepository>()
                .As<IHotToursWriteRepository>()
                .InstancePerDependency();

            builder
                .RegisterType<ChatWriteRepository>()
                .As<IChatWriteRepository>()
                .InstancePerDependency();

            builder
                .RegisterGeneric(typeof(ReadRepository<>))
                .As(typeof(IReadRepository<>));

            builder
                .RegisterType<CurrencyRateRepository>()
                .As<ICurrencyRateRepository>();

            builder
                .RegisterType<HotTourFoundRepository>()
                .As<IHotTourFoundRepository>();
        }
    }
}