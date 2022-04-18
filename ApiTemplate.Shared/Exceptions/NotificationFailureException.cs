using System;
using System.Runtime.Serialization;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class NotificationFailureException : Exception
{
    private const string DefaultMessage = "Notification has failed";

    public NotificationFailureException(string message = DefaultMessage) : this(message, null)
    {
    }

    public NotificationFailureException(Exception innerException) : this(DefaultMessage, innerException)
    {
    }

    public NotificationFailureException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected NotificationFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public static void When(bool condition, string message = DefaultMessage, Exception innerException = null)
    {
        if (condition)
            throw new NotificationFailureException(message, innerException);
    }
}
