using System.Diagnostics.CodeAnalysis;
using Autofac;
using BetterTravel.Api.ExceptionHandling;
using BetterTravel.Api.ExceptionHandling.Abstractions;
using MediatR;

namespace BetterTravel.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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