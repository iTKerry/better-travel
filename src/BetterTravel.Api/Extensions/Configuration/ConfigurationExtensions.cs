using Microsoft.Extensions.Configuration;

namespace BetterTravel.Api.Extensions.Configuration
{
    public static partial class ConfigurationExtensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) 
            where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }
    }
}