namespace TePay.Models.Requests;

/// <summary>
/// Represents a request to cancel a payment. The request includes the amount to be returned,
/// along with optional fields for split transactions (Extra and Extra2).
/// </summary>
public class CancelPaymentRequest
{
    /// <summary>
    /// Gets or sets the amount to be returned. This value should not exceed the original transaction amount.
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the Extra field for split transactions, which includes the IBAN number
    /// from whose account the amount should be transferred. It must follow the standard IBAN format.
    /// </summary>
    public string? Extra { get; set; }

    /// <summary>
    /// Gets or sets the Extra2 field for split transactions, which includes the amount to be canceled
    /// in the specified IBAN in Extra2 (e.g., 10; 10.50; 10.50).
    /// The amount in this field should not exceed the full amount to be canceled.
    /// </summary>
    public string? Extra2 { get; set; }
}
