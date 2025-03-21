using System.Text.Json.Serialization;

namespace TePay.Models.Responses;

/// <summary>
/// Represents an error response returned from the API.
/// Contains details about the error, including type, status, and additional error information.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Gets or sets the URI identifying a human-readable web page with more information about the error.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets a general description of the error.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the HTTP status code associated with the error.
    /// </summary>
    [JsonPropertyName("status")]
    public int? Status { get; set; }

    /// <summary>
    /// Gets or sets a human-readable text providing additional information about the error.
    /// </summary>
    [JsonPropertyName("detail")]
    public string? Detail { get; set; }

    /// <summary>
    /// Gets or sets the system error code in the format {method-name}.{http-status-code}.
    /// </summary>
    [JsonPropertyName("systemCode")]
    public string? SystemCode { get; set; }

    /// <summary>
    /// Gets or sets the error code associated with the specific error.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Gets or sets the business level transaction status description.
    /// This can provide detailed descriptions of error codes.
    /// </summary>
    [JsonPropertyName("resultCode")]
    public string? ResultCode { get; set; }

    /// <summary>
    /// Gets or sets the trace identifier for tracking the error.
    /// </summary>
    [JsonPropertyName("traceId")]
    public string? TraceId { get; set; }
}
