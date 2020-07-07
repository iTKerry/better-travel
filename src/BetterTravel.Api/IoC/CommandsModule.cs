﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Common.Specification;
using MediatR;

namespace BetterTravel.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class CommandsModule : Autofac.Module
    {
        protected override Assembly ThisAssembly => typeof(CommandHandlerBase<>).Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(Specification<,>))
                .AsImplementedInterfaces();
        }
    }
}