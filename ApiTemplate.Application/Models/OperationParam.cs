using ApiTemplate.Application.Enumerators;

namespace ApiTemplate.Application.Models;

public class OperationParam<T>
{
    public Operation Operation { get; set; }
    public T Value { get; set; }
}
