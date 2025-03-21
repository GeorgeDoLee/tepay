using FluentValidation;
using FluentValidation.Results;
using TePay.Exceptions;

namespace TePay.Helpers;

/// <summary>
/// Provides a utility to validate request models using FluentValidation.
/// This helper class allows you to validate any request model by passing the model and its associated validator.
/// If the validation fails, a <see cref="TePayValidationException"/> is thrown containing the validation errors.
/// </summary>
internal static class ValidatorHelper
{
    /// <summary>
    /// Validates a request model using the provided FluentValidator.
    /// If validation fails, a <see cref="TePayValidationException"/> is thrown containing the errors encountered during validation.
    /// </summary>
    /// <typeparam name="T">The type of the request model being validated.</typeparam>
    /// <param name="request">The request model to be validated.</param>
    /// <param name="validator">The validator instance used to validate the request model.</param>
    /// <exception cref="TePayValidationException">Thrown if the validation fails, containing a list of error messages.</exception>
    public static void Validate<T>(T request, IValidator<T> validator)
    {
        ValidationResult result = validator.Validate(request);

        if (!result.IsValid)
        {
            throw new TePayValidationException(request!.GetType().Name, result.Errors);
        }
    }
}
