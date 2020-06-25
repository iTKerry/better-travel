using System.Collections;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;

namespace BetterTravel.Common.Localization
{
    public class ResourceHelper
    {
        private ResourceManager ResourceManager { get; }
        
        public ResourceHelper() => 
            ResourceManager = Localization.ResourceManager;

        public string GetResourceName(string value)
        {
            var entry = ResourceManager
                .GetResourceSet(CultureInfo.GetCultureInfo("ru"), true, true)?
                .OfType<DictionaryEntry?>()
                .FirstOrDefault(dictionaryEntry =>
                    dictionaryEntry.Value.Value != null && dictionaryEntry.Value.ToString() == value);
            return entry?.ToString();
        }

        public string GetResourceValue(string name)
        {
            var value = ResourceManager.GetString(name);
            return !string.IsNullOrEmpty(value) ? value : null;
        }
    }
}