using System.Text.Json.Serialization;

namespace TePay.Models.Responses.Details;

/// <summary>
/// Represents the details of a recurring card used for payments.
/// Contains the saved card's recId, masked card PAN, and expiry date.
/// </summary>
public class RecurringCard
{
    /// <summary>
    /// Gets or sets the saved card's recId.
    /// This is used for initiating payments with the saved card.
    /// </summary>
    [JsonPropertyName("recId")]
    public string? RecId { get; set; }

    /// <summary>
    /// Gets or sets the masked card PAN (Primary Account Number).
    /// This is the masked version of the card number.
    /// </summary>
    [JsonPropertyName("cardMask")]
    public string? CardMask { get; set; }

    /// <summary>
    /// Gets or sets the expiry date of the saved card.
    /// </summary>
    [JsonPropertyName("expiryDate")]
    public string? ExpiryDate { get; set; }
}
