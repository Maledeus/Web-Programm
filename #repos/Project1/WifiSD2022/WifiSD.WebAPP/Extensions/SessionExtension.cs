using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace WifiSD.WebAPP.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            session.SetString(key, serializedValue);
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
