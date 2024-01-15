using System;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class ForbiddenException : Exception
{
    private const string DefaultMessage = "Access Forbidden";

    public ForbiddenException(string? message = DefaultMessage) : this(message, null)
    {
    }

    public ForbiddenException(Exception? innerException) : this(DefaultMessage, innerException)
    {
    }

    public ForbiddenException(string? message, Exception? innerException) : base(DefineMessage(message, DefaultMessage), innerException)
    {
    }

    private static string? DefineMessage(string? message, string? fallbackMessage)
    {
        return string.IsNullOrEmpty(message?.Trim()) ? fallbackMessage : message;
    }

    public static void ThrowIf(bool condition, string? message = DefaultMessage, Exception? innerException = null)
    {
        if (condition)
            throw new ForbiddenException(message, innerException);
    }
}
