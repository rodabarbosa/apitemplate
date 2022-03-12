using System;

namespace ApiTemplate.Shared.Exceptions
{
    public class ForbiddenException : Exception
    {
        private const string DefaultMessage = "Access Forbidden";

        public ForbiddenException(string message = DefaultMessage) : this(message, null)
        {
        }

        public ForbiddenException(Exception innerException) : this(DefaultMessage, innerException)
        {
        }

        public ForbiddenException(string message, Exception innerException) : base(message ?? DefaultMessage, innerException)
        {
        }

        public static void When(bool condition, string message = DefaultMessage, Exception innerException = null)
        {
            if (condition)
                throw new ForbiddenException(message, innerException);
        }
    }
}
