using System.Text.Json.Serialization;

namespace ApiTemplate.Shared.Models;

public sealed class ErrorModel
{
    [JsonPropertyName("code")] public int Code { get; init; }
    [JsonPropertyName("error")] public object? Error { get; init; }
    [JsonPropertyName("exception")] public string? Exception { get; init; }

    [JsonIgnore]
    [JsonPropertyName("stacktrace")]
    public string? StackTrace { get; init; }
}
