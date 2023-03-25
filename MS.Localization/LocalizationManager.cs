using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace MS.Localization
{
    public class LocalizationManager
    {
        public static readonly CultureInfo[] AllCultures;
        public static readonly string[] AllCultureNames;

        static LocalizationManager()
        {
            AllCultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            AllCultureNames = AllCultures.Select(t => t.Name.ToLower()).ToArray();
        }

        public static void ChangeCulture(string languageCode)
        {
            var dateTimeFormat =
                (DateTimeFormatInfo)
                CultureInfo.CreateSpecificCulture(ConfigurationManager.AppSettings["DefaultTemplateLanguage"])
                    .DateTimeFormat.Clone();
            CultureInfo ci;
            try
            {
                ci = new CultureInfo(languageCode) { DateTimeFormat = dateTimeFormat };
            }
            catch
            {
                languageCode = ConfigurationManager.AppSettings["DefaultTemplateLanguage"];
                ci = new CultureInfo(languageCode) { DateTimeFormat = dateTimeFormat };
            }
            if (ci.Name != Thread.CurrentThread.CurrentUICulture.Name)
                Thread.CurrentThread.CurrentUICulture = ci;

            if (ci.Name != Thread.CurrentThread.CurrentCulture.Name)
                Thread.CurrentThread.CurrentCulture = ci;
        }

        public static string Get(string name)
        {
            var retValue = Strings.ResourceManager.GetString(name);
            return retValue ?? name;
        }

        public static string GetByCulture(string name, string langCode)
        {
            var ci = new CultureInfo(langCode);
            var retValue = Strings.ResourceManager.GetString(name, ci);
            return retValue ?? name;
        }
    }
}
