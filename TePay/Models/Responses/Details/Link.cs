using System.Text.Json.Serialization;

namespace TePay.Models.Responses.Details;

/// <summary>
/// Represents a link associated with a payment. 
/// It contains information about the URL, HTTP method, and the action associated with the link.
/// </summary>
public class Link
{
    /// <summary>
    /// Gets or sets the URI of the link.
    /// This is the URL that the link points to.
    /// </summary>
    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    /// <summary>
    /// Gets or sets the HTTP method to be used with the URI.
    /// Examples include GET, POST, PUT, DELETE, etc.
    /// </summary>
    [JsonPropertyName("method")]
    public string? Method { get; set; }

    /// <summary>
    /// Gets or sets the relation type of the link.
    /// This describes the action that should be performed on the URL.
    /// Examples include "self", "next", "previous", etc.
    /// </summary>
    [JsonPropertyName("rel")]
    public string? Rel { get; set; }
}
