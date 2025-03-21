using FluentValidation.Results;

namespace TePay.Exceptions;

/// <summary>
/// Represents an exception that occurs when model validation fails.
/// </summary>
internal class TePayValidationException : Exception
{
    /// <summary>
    /// Gets the name of the object that failed validation.
    /// </summary>
    public string ObjectName { get; }

    /// <summary>
    /// Gets the list of validation error messages.
    /// </summary>
    public List<string> Errors { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TePayValidationException"/> class.
    /// </summary>
    /// <param name="objectName">The name of the object that failed validation.</param>
    /// <param name="failures">The list of validation failures.</param>
    public TePayValidationException(string objectName, List<ValidationFailure> failures)
        : base($"Validation failed for {objectName}.")
    {
        ObjectName = objectName;
        Errors = failures.Select(f => f.ErrorMessage).ToList();
    }
}
