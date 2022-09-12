using System;
using System.Runtime.Serialization;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    private const string DefaultMessage = "Not Found Exception";

    public NotFoundException(string? message = DefaultMessage) : this(message, null)
    {
    }

    public NotFoundException(Exception? innerException) : this(DefaultMessage, innerException)
    {
    }

    public NotFoundException(string? message, Exception? innerException) : base(DefineMessage(message, DefaultMessage), innerException)
    {
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    private static string? DefineMessage(string? message, string? fallbackMessage)
    {
        return string.IsNullOrEmpty(message?.Trim()) ? fallbackMessage : message;
    }

    public static void ThrowIf(bool condition, string? message = DefaultMessage, Exception? innerException = null)
    {
        if (condition)
            throw new NotFoundException(message, innerException);
    }
}
