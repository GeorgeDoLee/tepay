using TePay.Models.Requests.Details;

namespace TePay.Models.Requests;

/// <summary>
/// Represents a request to execute a recurring payment.
/// </summary>
public class ExecuteRecurringPaymentRequest
{
    /// <summary>
    /// Gets or sets the RecID of the saved card. This is required to process the recurring payment.
    /// </summary>
    public required string RecID { get; set; }

    /// <summary>
    /// Gets or sets an additional merchant-specific parameter. Only non-Unicode (ANSI) symbols are allowed. 
    /// Max length: 25 characters.
    /// </summary>
    public string? Extra { get; set; }

    /// <summary>
    /// Gets or sets an additional merchant-specific parameter. Only non-Unicode (ANSI) symbols are allowed. 
    /// Max length: 52 characters.
    /// </summary>
    public string? Extra2 { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether preauthorization is needed. 
    /// If set to true, the amount will be blocked on the card, 
    /// and an additional request must be sent to complete the payment.
    /// </summary>
    public bool? PreAuth { get; set; }

    /// <summary>
    /// Gets or sets the merchant-side identifier for the payment.
    /// </summary>
    public string? MerchantPaymentId { get; set; }

    /// <summary>
    /// Gets or sets the initiator of the transaction.
    /// If set to "merchant", the transaction is merchant-initiated. 
    /// If not provided, it will be considered a customer-initiated transaction.
    /// </summary>
    public string? Initiator { get; set; }

    /// <summary>
    /// Gets or sets the monetary details of the payment, including amount and currency.
    /// </summary>
    public required Money Money { get; set; }
}
