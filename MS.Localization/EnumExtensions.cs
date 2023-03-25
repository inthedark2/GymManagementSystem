using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MS.Localization
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static Int32 GetInt32Value<T>(this T t) where T : struct
        {
            return Convert.ToInt32(t);
        }

        public static String GetLocalizedValue<T>(this T t) where T : struct
        {
            return LocalizationManager.Get($"Enum_{t.GetType().Name}_{t}");
        }

        public static IEnumerable<string> GetLocalizedValues<T>() where T : struct
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>().Select(x => x.GetLocalizedValue());
            return list;
        }

        public static IEnumerable<T> GetEnumList<T>() where T : struct
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>();
            return list;
        }

        public static IEnumerable<object> GetEnumDropdownCollection<T>() where T : struct
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>().Select(x => new { Id = x.GetInt32Value(), Name = x.GetLocalizedValue() });
            return list;
        }

        public static IEnumerable<SelectListItem> GetEnumSelectListItemCollection<T>() where T : struct
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>().Select(x => new SelectListItem { Value = x.GetInt32Value().ToString(), Text = x.GetLocalizedValue() });
            return list;
        }

        public static IEnumerable<object> GetEnumDropdownCollection<T>(IList<T> exceptList) where T : struct
        {
            var policiesCoverage = Enum.GetValues(typeof(T)).Cast<T>().Select(x => new { Id = x.GetInt32Value(), Name = x.GetLocalizedValue() }).Where(x => !exceptList.Select(y => y.GetInt32Value()).Contains(x.Id));
            return policiesCoverage;
        }

        public static IEnumerable<object> GetEnumDropdownCollectionFromEnumList<T>(IList<T> enumList) where T : struct
        {
            var policiesCoverage = enumList.Select(x => new { Id = x.GetInt32Value(), Name = x.GetLocalizedValue() });
            return policiesCoverage;
        }

        public static string SerializeEnum<T>()
        {
            var type = typeof(T);
            var values = Enum.GetValues(type).Cast<T>();
            var dict = values.ToDictionary(e => e.ToString(), e => Convert.ToInt32(e));
            var json = new JavaScriptSerializer().Serialize(dict);
            var script = $"var {type.Name}={json};";
            return script;
        }
    }
}