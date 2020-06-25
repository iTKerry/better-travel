using System.Collections;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace BetterTravel.Common.Localization
{
    public static class ResourceManagerHelper
    {
        public static string GetResourceName(this ResourceManager resourceManager, string value,
            CultureInfo cultureInfo, bool ignoreCase = false)
        {
            var comparisonType =
                ignoreCase ? System.StringComparison.OrdinalIgnoreCase : System.StringComparison.Ordinal;
            var entry = resourceManager.GetResourceSet(cultureInfo, true, true)?
                .OfType<DictionaryEntry>()
                .FirstOrDefault(de => 
                    de.Value != null && 
                    de.Value.ToString().Equals(value, comparisonType));

            return entry?.Key?.ToString();
        }
    }
}