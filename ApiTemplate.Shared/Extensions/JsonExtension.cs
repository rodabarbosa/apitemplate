using System.Text.Encodings.Web;
using System.Text.Json;

namespace ApiTemplate.Shared.Extensions
{
    public static class JsonExtension
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false,
            Encoder = JavaScriptEncoder.Default,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true
        };

        public static string ToJson(this object value) => JsonSerializer.Serialize(value);

        public static T FromJson<T>(this string value) where T : class => JsonSerializer.Deserialize<T>(value, _jsonSerializerOptions);
    }
}
