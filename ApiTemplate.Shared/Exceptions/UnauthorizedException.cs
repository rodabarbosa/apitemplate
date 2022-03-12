using System;

namespace ApiTemplate.Shared.Exceptions
{
    public class UnauthorizedException : Exception
    {
        private const string DefaultMessage = "Unauthorized Access";

        public UnauthorizedException(string message = DefaultMessage) : this(message, null)
        {
        }

        public UnauthorizedException(Exception innerException) : this(DefaultMessage, innerException)
        {
        }

        public UnauthorizedException(string message, Exception innerException) : base(message ?? DefaultMessage, innerException)
        {
        }

        public static void When(bool condition, string message = DefaultMessage, Exception innerException = null)
        {
            if (condition)
                throw new UnauthorizedException(message, innerException);
        }
    }
}
