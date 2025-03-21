using System.Text.Json.Serialization;

namespace TePay.Models.Requests.Details;

/// <summary>
/// Represents the payment amount details including the currency, total,
/// and optional breakdowns for subtotal, tax, and shipping.
/// </summary>
public class Amount
{
    /// <summary>
    /// transaction currency (3 digit ISO code).
    /// The following values are allowed: GEL, USD, EUR
    /// </summary>
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    /// <summary>
    /// total amount of payment
    /// </summary>
    [JsonPropertyName("total")]
    public required decimal Total { get; set; }

    /// <summary>
    /// amount of purchase
    /// </summary>
    [JsonPropertyName("subTotal")]
    public decimal? SubTotal { get; set; }

    /// <summary>
    /// amount of tax
    /// </summary>
    [JsonPropertyName("tax")]
    public decimal? Tax { get; set; }

    /// <summary>
    /// amount of shipping price
    /// </summary>
    [JsonPropertyName("shipping")]
    public decimal? Shipping { get; set; }
}
