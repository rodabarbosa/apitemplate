using System;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class NotificationFailureException : Exception
{
    private const string DefaultMessage = "Notification has failed";

    public NotificationFailureException(string? message = DefaultMessage) : this(message, null)
    {
    }

    public NotificationFailureException(Exception? innerException) : this(DefaultMessage, innerException)
    {
    }

    public NotificationFailureException(string? message, Exception? innerException) : base(DefineMessage(message, DefaultMessage), innerException)
    {
    }

    private static string? DefineMessage(string? message, string? fallbackMessage)
    {
        return string.IsNullOrEmpty(message?.Trim()) ? fallbackMessage : message;
    }

    public static void ThrowIf(bool condition, string? message = DefaultMessage, Exception? innerException = null)
    {
        if (condition)
            throw new NotificationFailureException(message, innerException);
    }
}
