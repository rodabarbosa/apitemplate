using System;

namespace ApiTemplate.Shared.Exceptions
{
    public class DeleteFailureException : Exception
    {
        private const string DefaultMessage = "Delete has failed";

        public DeleteFailureException(string message = DefaultMessage) : this(message, null)
        {
        }

        public DeleteFailureException(Exception innerException) : this(DefaultMessage, innerException)
        {
        }

        public DeleteFailureException(string message, Exception innerException) : base(message ?? DefaultMessage, innerException)
        {
        }

        public static void When(bool condition, string message = DefaultMessage, Exception innerException = null)
        {
            if (condition)
                throw new DeleteFailureException(message, innerException);
        }
    }
}
