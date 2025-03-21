using System.Text.Json.Serialization;

namespace TePay.Models.Responses;

/// <summary>
/// Represents the response received when completing the pre-authorization payment.
/// </summary>
public class CompletePreAuthPaymentResponse
{
    /// <summary>
    /// Gets or sets the status of the payment completion.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the total amount for the payment (as provided in the completion request).
    /// This value should not exceed the transaction amount from the initial pre-authorization.
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Gets or sets the amount that was confirmed as part of the payment completion.
    /// If the amount provided in the request is less than the original transaction amount, 
    /// this field will reflect the confirmed amount.
    /// </summary>
    [JsonPropertyName("confirmedAmount")]
    public decimal? ConfirmedAmount { get; set; }

    /// <summary>
    /// Gets or sets the HTTP status code returned by the API in response to the request.
    /// </summary>
    [JsonPropertyName("httpStatusCode")]
    public int? HttpStatusCode { get; set; }

    /// <summary>
    /// Gets or sets a developer-specific message, which can provide additional details on the outcome of the request.
    /// </summary>
    [JsonPropertyName("developerMessage")]
    public string? DeveloperMessage { get; set; }

    /// <summary>
    /// Gets or sets a user-facing message that may be displayed to the end user to inform them of the payment completion status.
    /// </summary>
    [JsonPropertyName("userMessage")]
    public string? UserMessage { get; set; }
}
