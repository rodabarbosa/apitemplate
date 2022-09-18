using System;
using System.Runtime.Serialization;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class BadRequestException : Exception
{
    private const string DefaultMessage = "Bad Request";

    public BadRequestException(string? message = DefaultMessage) : this(message, default)
    {
    }

    public BadRequestException(Exception? innerException) : this(DefaultMessage, innerException)
    {
    }

    public BadRequestException(string? message, Exception? innerException) : base(DefineMessage(message, DefaultMessage), innerException)
    {
    }

    protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    private static string? DefineMessage(string? message, string? fallbackMessage)
    {
        return string.IsNullOrEmpty(message?.Trim()) ? fallbackMessage : message;
    }

    public static void ThrowIf(bool condition, string? message = DefaultMessage, Exception? innerException = null)
    {
        if (condition)
            throw new BadRequestException(message, innerException);
    }
}
