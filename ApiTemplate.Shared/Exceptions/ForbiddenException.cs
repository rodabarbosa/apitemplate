using System;
using System.Runtime.Serialization;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class ForbiddenException : Exception
{
    private const string DefaultMessage = "Access Forbidden";

    public ForbiddenException(string message = DefaultMessage) : this(message, null)
    {
    }

    public ForbiddenException(Exception innerException) : this(DefaultMessage, innerException)
    {
    }

    public ForbiddenException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public static void ThrowIf(bool condition, string message = DefaultMessage, Exception innerException = null)
    {
        if (condition)
            throw new ForbiddenException(message, innerException);
    }
}
