using System;
using System.Runtime.Serialization;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class DbRegisterExistsException : Exception
{
    private const string DefaultMessage = "A similar register already exists";

    public DbRegisterExistsException(string message = DefaultMessage) : this(message, null)
    {
    }

    public DbRegisterExistsException(Exception innerException) : this(DefaultMessage, innerException)
    {
    }

    public DbRegisterExistsException(string message, Exception innerException) : base(message ?? DefaultMessage, innerException)
    {
    }

    protected DbRegisterExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public static void When(bool condition, string message = DefaultMessage, Exception innerException = null)
    {
        if (condition)
            throw new DbRegisterExistsException(message, innerException);
    }
}
