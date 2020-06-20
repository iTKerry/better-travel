using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using BetterTravel.Queries.Abstractions;
using MediatR;

namespace BetterTravel.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class QueriesModule : Autofac.Module
    {
        protected override Assembly ThisAssembly => typeof(QueryHandlerBase<,>).Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();
        }
    }
}