using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WifiSD.Common.Extensions
{
    namespace SD.Common.Extensions
    {
        public static class EnumExtensions
        {
            public static string GetDescription<T>(this T value)
            where T : Enum
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                var descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.Description;
                }
                return null;
            }

            public static IEnumerable<string> GetDescriptions<T>()
            where T : Enum
            {
                return Enum.GetValues(typeof(T)).Cast<T>().Select(s => GetDescription(s));

            }

            public static List<KeyValuePair<object, string>> EnumToList<TEnum>()
            where TEnum : Enum
            {
                var result = new List<KeyValuePair<object, string>>();
                var enumType = typeof(TEnum);
                foreach (Enum _enum in Enum.GetValues(enumType))
                {
                    result.Add(new KeyValuePair<object, string>((object)_enum, GetDescription(_enum)));
                }
                return result;
            }
        }
    }
}
