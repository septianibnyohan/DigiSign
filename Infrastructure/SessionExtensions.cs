using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace DigiSign.Infrastructure {
    public static class SessionExtensions {
        public static void Set(this ISession session, string key, string value) {
            session.SetString(key, value);
        }
        public static string Get(this ISession session, string key) {
            var sessionData = session.GetString(key);
            return sessionData;
        }
    }
}