using System;
using System.Runtime.Serialization;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class SaveFailureException : Exception
{
    private const string DefaultMessage = "Save has failed";

    public SaveFailureException(string message = DefaultMessage) : this(message, null)
    {
    }

    public SaveFailureException(Exception innerException) : this(DefaultMessage, innerException)
    {
    }

    public SaveFailureException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected SaveFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public static void ThrowIf(bool condition, string message = DefaultMessage, Exception innerException = null)
    {
        if (condition)
            throw new SaveFailureException(message, innerException);
    }
}
