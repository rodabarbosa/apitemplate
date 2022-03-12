using System;

namespace ApiTemplate.Shared.Exceptions
{
    public class SaveFailureException : Exception
    {
        private const string DefaultMessage = "Save has failed";

        public SaveFailureException(string message = DefaultMessage) : this(message, null)
        {
        }

        public SaveFailureException(Exception innerException) : this(DefaultMessage, innerException)
        {
        }

        public SaveFailureException(string message, Exception innerException) : base(message ?? DefaultMessage, innerException)
        {
        }

        public static void When(bool condition, string message = DefaultMessage, Exception innerException = null)
        {
            if (condition)
                throw new SaveFailureException(message, innerException);
        }
    }
}
