using System.Reflection;
using Autofac;
using BetterTravel.DataAccess.Abstraction.Repositories;
using BetterTravel.DataAccess.EF;
using BetterTravel.DataAccess.EF.Repositories;
using Module = Autofac.Module;

namespace BetterTravel.Api.IoC
{
    public class DataAccessModule : Module
    {
        protected override Assembly ThisAssembly => typeof(AppDbContext).Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HotToursRepository>()
                .As<IHotToursRepository>();

            builder
                .RegisterType<ChatRepository>()
                .As<IChatRepository>();
        }
    }
}