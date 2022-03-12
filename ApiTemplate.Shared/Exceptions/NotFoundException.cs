﻿using System;

namespace ApiTemplate.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        private const string DefaultMessage = "Not Found Exception";

        public NotFoundException(string message = DefaultMessage) : this(message, null)
        {
        }

        public NotFoundException(Exception innerException) : this(DefaultMessage, innerException)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message ?? DefaultMessage, innerException)
        {
        }

        public static void When(bool condition, string message = DefaultMessage, Exception innerException = null)
        {
            if (condition)
                throw new DeleteFailureException(message, innerException);
        }
    }
}
