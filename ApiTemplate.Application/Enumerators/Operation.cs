using System.Text.Json.Serialization;

namespace ApiTemplate.Application.Enumerators;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Operation
{
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Contains,
    StartsWith,
    EndsWith
}
