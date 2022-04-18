using System;
using System.Runtime.Serialization;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class UnauthorizedException : Exception
{
    private const string DefaultMessage = "Unauthorized Access";

    public UnauthorizedException(string message = DefaultMessage) : this(message, null)
    {
    }

    public UnauthorizedException(Exception innerException) : this(DefaultMessage, innerException)
    {
    }

    public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public static void When(bool condition, string message = DefaultMessage, Exception innerException = null)
    {
        if (condition)
            throw new UnauthorizedException(message, innerException);
    }
}
