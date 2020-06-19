using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BetterTravel.Common.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBetterTravelSwagger(this IServiceCollection services) => 
            services.AddSwaggerGen(SetupSwaggerGenOptions);

        private static void SetupSwaggerGenOptions(SwaggerGenOptions options)
        {
            var assembly = typeof(Startup).Assembly;
            var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
            var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

            options.DescribeAllParametersInCamelCase();
            options.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Pattern = DateFormat.Pattern });

            options.SwaggerDoc(ApiVersions.V1, new OpenApiInfo
            {
                Version = ApiVersions.V1,
                Title = assemblyProduct,
                Description = assemblyDescription
            });

            foreach (var path in GetXmlPaths())
            {
                options.IncludeXmlComments(path);
            }
        }
        
        private static IEnumerable<string> GetXmlPaths() =>
            DependencyContext.Default.GetDefaultAssemblyNames()
                .Where(assembly => assembly.FullName.StartsWith("BetterTravel", StringComparison.InvariantCulture))
                .Select(p => Path.Combine(AppContext.BaseDirectory, $"{p.Name}.xml"))
                .Where(File.Exists);
    }
}