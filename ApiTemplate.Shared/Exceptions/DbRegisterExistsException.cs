using System;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class DbRegisterExistsException : Exception
{
    private const string DefaultMessage = "A similar register already exists";

    public DbRegisterExistsException(string? message = DefaultMessage) : this(message, null)
    {
    }

    public DbRegisterExistsException(Exception? innerException) : this(DefaultMessage, innerException)
    {
    }

    public DbRegisterExistsException(string? message, Exception? innerException) : base(DefineMessage(message, DefaultMessage), innerException)
    {
    }

    private static string? DefineMessage(string? message, string? fallbackMessage)
    {
        return string.IsNullOrEmpty(message?.Trim()) ? fallbackMessage : message;
    }

    public static void ThrowIf(bool condition, string? message = DefaultMessage, Exception? innerException = null)
    {
        if (condition)
            throw new DbRegisterExistsException(message, innerException);
    }
}
