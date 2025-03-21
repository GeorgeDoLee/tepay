using System.Text.Json.Serialization;

namespace TePay.Models.Requests.Details;

/// <summary>
/// Represents monetary details for a payment.
/// </summary>
public class Money
{
    /// <summary>
    /// Gets or sets the amount of the payment.
    /// </summary>
    [JsonPropertyName("amount")]
    public required decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the currency of the payment in a 3-letter ISO format (e.g., GEL, USD, EUR).
    /// </summary>
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }
}