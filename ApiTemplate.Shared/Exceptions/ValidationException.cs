using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ApiTemplate.Shared.Exceptions;

[Serializable]
public class ValidationException : Exception
{
    private const string DefaultMessage = "One or more validation failure have occurred.";

    private ValidationException(string message = DefaultMessage) : this(message, null)
    {
    }

    public ValidationException(Exception? innerException) : this(DefaultMessage, innerException)
    {
    }

    private ValidationException(string? message, Exception? innerException) : base(DefineMessage(message, DefaultMessage), innerException)
    {
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        IEnumerable<ValidationFailure> validationFailures = failures.ToList();
        var propertyNames = validationFailures
            .Select(e => e.PropertyName)
            .Distinct();

        foreach (var propertyName in propertyNames)
        {
            var propertyFailures = validationFailures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            Failures.Add(propertyName, propertyFailures);
        }
    }

    protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public IDictionary<string, string[]> Failures { get; } = new Dictionary<string, string[]>();

    static private string? DefineMessage(string? message, string? fallbackMessage)
    {
        return string.IsNullOrEmpty(message?.Trim()) ? fallbackMessage : message;
    }

    static public void ThrowIf(bool condition, IEnumerable<ValidationFailure> failures)
    {
        if (condition)
            throw new ValidationException(failures);
    }

    static public void ThrowIf(bool condition, string? message = DefaultMessage, Exception? innerException = null)
    {
        if (condition)
            throw new ValidationException(message, innerException);
    }
}
