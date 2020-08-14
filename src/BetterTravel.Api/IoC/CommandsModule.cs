using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace BetterTravel.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class CommandsModule : Autofac.Module
    {
        //protected override Assembly ThisAssembly => typeof(CommandHandlerBase<>).Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            /*
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();
            */
        }
    }
}