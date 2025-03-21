using Serilog;
using TePay.Exceptions;

namespace TePay.ErrorHandling;

/// <summary>
/// A static class that provides error handling functionality.
/// This class is designed to wrap around the entry points (e.g., methods in TePayService) 
/// to handle specific exceptions (validation, serialization, API errors) and log them accordingly.
/// </summary>
internal static class ErrorHandler
{
    /// <summary>
    /// Asynchronously executes the provided action, logs any exceptions that occur,
    /// and rethrows them to be handled by the caller.
    /// </summary>
    /// <typeparam name="T">The return type of the action.</typeparam>
    /// <param name="action">The action to execute asynchronously.</param>
    /// <param name="logger">The logger to log any errors encountered.</param>
    /// <returns>The result of the action if it succeeds.</returns>
    /// <exception cref="TePayValidationException">Thrown when request validation fails.</exception>
    /// <exception cref="TePaySerializationException">Thrown when JSON serialization/deserialization fails.</exception>
    /// <exception cref="TePayApiException">Thrown when an API exception occurs.</exception>
    /// <exception cref="Exception">Thrown for any unexpected exceptions.</exception>
    public static async Task<T> HandleAsync<T>(Func<Task<T>> action, ILogger logger)
    {
        return await HandleInternalAsync(action, logger);
    }

    /// <summary>
    /// Asynchronously executes the provided action, logs any exceptions that occur,
    /// and rethrows them to be handled by the caller. This method is used for actions that don't return a result.
    /// </summary>
    /// <param name="action">The action to execute asynchronously.</param>
    /// <param name="logger">The logger to log any errors encountered.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="TePayValidationException">Thrown when request validation fails.</exception>
    /// <exception cref="TePaySerializationException">Thrown when JSON serialization/deserialization fails.</exception>
    /// <exception cref="TePayApiException">Thrown when an API exception occurs.</exception>
    /// <exception cref="Exception">Thrown for any unexpected exceptions.</exception>
    public static async Task HandleAsync(Func<Task> action, ILogger logger)
    {
        await HandleInternalAsync(async () =>
        {
            await action();
            return true;
        }, logger);
    }

    /// <summary>
    /// Internal method to execute the provided action and handle exceptions.
    /// This method wraps the core logic for handling various types of exceptions
    /// and logging them before rethrowing.
    /// </summary>
    /// <typeparam name="T">The return type of the action.</typeparam>
    /// <param name="action">The action to execute asynchronously.</param>
    /// <param name="logger">The logger to log any errors encountered.</param>
    /// <returns>The result of the action if it succeeds.</returns>
    private static async Task<T> HandleInternalAsync<T>(Func<Task<T>> action, ILogger logger)
    {
        try
        {
            return await action();
        } 
        catch (TePayAuthenticationException ex)
        {
            logger.Error(ex, "TePay API authentication exception occurred. {@ErrorResponse}", ex.ErrorResponse);
            throw;
        }
        catch (TePayValidationException ex)
        {
            logger.Error(ex, "TePay validation exception occurred. {@Errors}", ex.Errors);
            throw;
        }
        catch (TePaySerializationException ex)
        {
            logger.Error(ex, "TePay serialization exception occurred.");
            throw;
        }
        catch (TePayApiException ex)
        {
            logger.Error(ex, "TePay API exception occurred. {@ErrorResponse}", ex.ErrorResponse);
            throw;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "An unexpected error occurred.");
            throw;
        }
    }
}
