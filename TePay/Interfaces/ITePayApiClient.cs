namespace TePay.Interfaces;

/// <summary>
/// Defines the operations for interacting with the TBC Payment API.
/// </summary>
public interface ITePayApiClient
{
    /// <summary>
    /// Sends an HTTP request to the specified API endpoint.
    /// This method sends a request without expecting a return value, only ensuring the request is sent.
    /// </summary>
    /// <param name="method">The HTTP method to be used (GET, POST, etc.).</param>
    /// <param name="endpoint">The specific API endpoint for the request.</param>
    /// <param name="content">The content of the request (optional). This will be serialized to JSON if provided.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendRequestAsync(HttpMethod method, string endpoint, object content = null!);

    /// <summary>
    /// Sends an HTTP request to the specified API endpoint and expects a response.
    /// This method sends a request and deserializes the response into the specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the response will be deserialized.</typeparam>
    /// <param name="method">The HTTP method to be used (GET, POST, etc.).</param>
    /// <param name="endpoint">The specific API endpoint for the request.</param>
    /// <param name="content">The content of the request (optional). This will be serialized to JSON if provided.</param>
    /// <returns>A task representing the asynchronous operation, returning the deserialized response of type <typeparamref name="T"/>.</returns>
    Task<T> SendRequestAsync<T>(HttpMethod method, string endpoint, object content = null!);
}
