using System.Globalization;

namespace BetterTravel.Common.Localization
{
    public static class L
    {
        public static string GetValue(string name, Culture culture = Culture.Default) => 
            Localization.ResourceManager.GetString(name, GetCultureInfo(culture)) ?? name;

        public static string GetName(string value, Culture culture, bool ignoreCase = false) =>
            Localization.ResourceManager.GetResourceName(value, GetCultureInfo(culture), ignoreCase) ?? value;

        private static CultureInfo GetCultureInfo(Culture culture) =>
            culture switch
            {
                Culture.Ru => new CultureInfo("ru-RU"),
                _ => CultureInfo.CurrentCulture
            };
    }

    public enum Culture
    {
        Default,
        Ru
    }
}