using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace ApiTemplate.Shared.Exceptions;

public class ValidationException : Exception
{
    private const string DefaultMessage = "One or more validation failure have occurred.";

    private ValidationException(string message = DefaultMessage) : this(message, null)
    {
    }

    public ValidationException(Exception innerException) : this(DefaultMessage, innerException)
    {
    }

    private ValidationException(string message, Exception innerException) : base(message ?? DefaultMessage, innerException)
    {
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        IEnumerable<string> propertyNames = failures
            .Select(e => e.PropertyName)
            .Distinct();

        foreach (string propertyName in propertyNames)
        {
            string[] propertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            Failures.Add(propertyName, propertyFailures);
        }
    }

    public IDictionary<string, string[]> Failures { get; } = new Dictionary<string, string[]>();

    public static void When(bool condition, IEnumerable<ValidationFailure> failures)
    {
        if (condition)
            throw new ValidationException(failures);
    }

    public static void When(bool condition, string message = DefaultMessage, Exception innerException = null)
    {
        if (condition)
            throw new ValidationException(message, innerException);
    }
}
