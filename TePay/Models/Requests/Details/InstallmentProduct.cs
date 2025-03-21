using System.Text.Json.Serialization;

namespace TePay.Models.Requests.Details;

/// <summary>
/// Represents a product included in an installment payment.
/// </summary>
public class InstallmentProduct
{
    /// <summary>
    /// Product description
    /// </summary>
    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    /// <summary>
    /// Product price in Lari 
    /// </summary>
    [JsonPropertyName("Price")]
    public required decimal Price { get; set; }

    /// <summary>
    /// Number of purchased units.
    /// </summary>
    [JsonPropertyName("Quantity")]
    public required int Quantity { get; set; }
}
