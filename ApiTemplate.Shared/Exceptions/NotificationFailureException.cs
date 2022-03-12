using System;

namespace ApiTemplate.Shared.Exceptions
{
    public class NotificationFailureException : Exception
    {
        private const string DefaultMessage = "Notification has failed";

        public NotificationFailureException(string message = DefaultMessage) : this(message, null)
        {
        }

        public NotificationFailureException(Exception innerException) : this(DefaultMessage, innerException)
        {
        }

        public NotificationFailureException(string message, Exception innerException) : base(message ?? DefaultMessage, innerException)
        {
        }

        public static void When(bool condition, string message = DefaultMessage, Exception innerException = null)
        {
            if (condition)
                throw new NotificationFailureException(message, innerException);
        }
    }
}
