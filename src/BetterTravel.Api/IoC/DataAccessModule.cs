using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using BetterTravel.DataAccess.Abstraction.Repositories;
using BetterTravel.DataAccess.EF;
using BetterTravel.DataAccess.EF.Repositories;
using BetterTravel.DataAccess.EF.Seeder;
using BetterTravel.DataAccess.EF.Seeder.Abstractions;
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
                .RegisterType<HotToursRepository>()
                .As<IHotToursRepository>();

            builder
                .RegisterType<ChatRepository>()
                .As<IChatRepository>();

            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>();
        }
    }
}