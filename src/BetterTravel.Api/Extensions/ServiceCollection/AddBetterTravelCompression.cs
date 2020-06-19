using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace BetterTravel.Api.Extensions.ServiceCollection
{
    public static partial class ServiceCollectionExtensions
    {
        private const CompressionLevel CompressionLevel = System.IO.Compression.CompressionLevel.Optimal;

        public static IServiceCollection AddBetterTravelCompression(this IServiceCollection services) =>
            services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel)
                .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel)
                .AddResponseCompression(SetupResponseCompressionOptions);

        private static void SetupResponseCompressionOptions(ResponseCompressionOptions options)
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        }
    }
}