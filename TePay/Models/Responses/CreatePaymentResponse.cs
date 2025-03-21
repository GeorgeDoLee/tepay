using System.Text.Json.Serialization;
using TePay.Models.Responses.Details;

namespace TePay.Models.Responses;

/// <summary>
/// Represents the response received after creating a payment.
/// </summary>
public class CreatePaymentResponse
{
    /// <summary>
    /// Gets or sets the payment identifier (payId).
    /// </summary>
    [JsonPropertyName("payId")]
    public string? PayId { get; set; }

    /// <summary>
    /// Gets or sets the payment status.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the transaction currency (3-digit ISO code).
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// Gets or sets the transaction amount. 
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Gets or sets the confirmed amount.
    /// </summary>
    [JsonPropertyName("confirmedAmount")]
    public decimal? ConfirmedAmount { get; set; }

    /// <summary>
    /// Gets or sets the list of links associated with the payment.
    /// Each link includes a URL, HTTP method, and an action to be performed.
    /// </summary>
    [JsonPropertyName("links")]
    public List<Link>? Links { get; set; }

    /// <summary>
    /// Gets or sets the transaction ID from UFC (Unique Financial Code).
    /// </summary>
    [JsonPropertyName("transactionId")]
    public string? TransactionId { get; set; }

    /// <summary>
    /// Gets or sets the recurring payment ID (recId).
    /// </summary>
    [JsonPropertyName("recId")]
    public string? RecId { get; set; }

    /// <summary>
    /// Gets or sets the preauthorization status for the given payment. 
    /// A boolean value indicating whether the payment is preauthorized.
    /// </summary>
    [JsonPropertyName("preAuth")]
    public bool? PreAuth { get; set; }

    /// <summary>
    /// Gets or sets the HTTP status code of the response.
    /// </summary>
    [JsonPropertyName("httpStatusCode")]
    public int? HttpStatusCode { get; set; }

    /// <summary>
    /// Gets or sets the developer message for logging in the local system.
    /// </summary>
    [JsonPropertyName("developerMessage")]
    public string? DeveloperMessage { get; set; }

    /// <summary>
    /// Gets or sets the error message intended for the user in case of any issues with the payment.
    /// </summary>
    [JsonPropertyName("userMessage")]
    public string? UserMessage { get; set; }

    /// <summary>
    /// Gets or sets the expiration time (in minutes) for the payment initiation.
    /// After this time, the payment initiation expires.
    /// </summary>
    [JsonPropertyName("expirationMinutes")]
    public int? ExpirationMinutes { get; set; }
}
