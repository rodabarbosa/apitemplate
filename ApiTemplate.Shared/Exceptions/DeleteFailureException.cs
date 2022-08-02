using System;
using System.Runtime.Serialization;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class DeleteFailureException : Exception
{
    private const string DefaultMessage = "Delete has failed";

    public DeleteFailureException(string message = DefaultMessage) : this(message, null)
    {
    }

    public DeleteFailureException(Exception innerException) : this(DefaultMessage, innerException)
    {
    }

    public DeleteFailureException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected DeleteFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public static void ThrowIf(bool condition, string message = DefaultMessage, Exception innerException = null)
    {
        if (condition)
            throw new DeleteFailureException(message, innerException);
    }
}
