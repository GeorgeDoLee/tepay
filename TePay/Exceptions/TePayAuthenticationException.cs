using System.Net;
using TePay.Models.Responses;

namespace TePay.Exceptions;

/// <summary>
/// Represents an exception that occurs when the TBC Payment API authentication returns an error response.
/// </summary>
internal class TePayAuthenticationException : Exception
{
    /// <summary>
    /// Gets the HTTP status code returned by the API.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the detailed error response from the API, if available.
    /// </summary>
    public ErrorResponse? ErrorResponse { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TePayApiException"/> class with the specified status code and error response.
    /// </summary>
    /// <param name="statusCode">The HTTP status code returned by the API.</param>
    /// <param name="errorResponse">The detailed error response from the API.</param>
    public TePayAuthenticationException(HttpStatusCode statusCode, ErrorResponse errorResponse)
        : base($"API Error {statusCode}: {errorResponse?.Title ?? "Unknown error"}")
    {
        StatusCode = statusCode;
        ErrorResponse = errorResponse;
    }
}
