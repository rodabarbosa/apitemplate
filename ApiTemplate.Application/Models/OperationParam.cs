using ApiTemplate.Application.Enumerators;

namespace ApiTemplate.Application.Models;

/// <summary>
/// Container for search params in list methods
/// </summary>
/// <typeparam name="T"></typeparam>
public class OperationParam<T>
{
    /// <summary>
    /// Defines operation type
    /// </summary>
    public Operation Operation { get; set; }

    /// <summary>
    /// Defines search value
    /// </summary>
    public T Value { get; set; }
}
