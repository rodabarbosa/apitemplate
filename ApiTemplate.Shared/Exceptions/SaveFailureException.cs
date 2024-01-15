using System;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class SaveFailureException : Exception
{
    private const string DefaultMessage = "Save has failed";

    public SaveFailureException(string? message = DefaultMessage) : this(message, null)
    {
    }

    public SaveFailureException(Exception? innerException) : this(DefaultMessage, innerException)
    {
    }

    public SaveFailureException(string? message, Exception? innerException) : base(DefineMessage(message, DefaultMessage), innerException)
    {
    }

    private static string? DefineMessage(string? message, string? fallbackMessage)
    {
        return string.IsNullOrEmpty(message?.Trim()) ? fallbackMessage : message;
    }

    public static void ThrowIf(bool condition, string? message = DefaultMessage, Exception? innerException = null)
    {
        if (condition)
            throw new SaveFailureException(message, innerException);
    }
}
