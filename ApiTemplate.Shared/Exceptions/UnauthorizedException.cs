using System;
using System.Runtime.Serialization;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class UnauthorizedException : Exception
{
    private const string DefaultMessage = "Unauthorized Access";

    public UnauthorizedException(string? message = DefaultMessage) : this(message, null)
    {
    }

    public UnauthorizedException(Exception? innerException) : this(DefaultMessage, innerException)
    {
    }

    public UnauthorizedException(string? message, Exception? innerException) : base(DefineMessage(message, DefaultMessage), innerException)
    {
    }

    protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    private static string? DefineMessage(string? message, string? fallbackMessage)
    {
        return string.IsNullOrEmpty(message?.Trim()) ? fallbackMessage : message;
    }

    public static void ThrowIf(bool condition, string? message = DefaultMessage, Exception? innerException = null)
    {
        if (condition)
            throw new UnauthorizedException(message, innerException);
    }
}
