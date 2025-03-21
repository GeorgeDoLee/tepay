using System.Text.Json.Serialization;
using TePay.Models.Responses.Details;

namespace TePay.Models.Responses
{
    /// <summary>
    /// Represents the detailed response for a payment transaction.
    /// Contains various details about the transaction such as status, amount, and card details.
    /// </summary>
    public class PaymentDetailsResponse
    {
        /// <summary>
        /// Gets or sets the payment ID associated with the transaction.
        /// </summary>
        [JsonPropertyName("payId")]
        public string? PayId { get; set; }

        /// <summary>
        /// Gets or sets the status of the payment transaction.
        /// The following values are allowed:
        /// Created, Processing, Succeeded, Failed, Expired, WaitingConfirm,
        /// CancelPaymentProcessing, PaymentCompletionProcessing, Returned, PartialReturned
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the currency of the transaction, in 3-digit ISO code.
        /// </summary>
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the confirmed amount in case of preauthorization.
        /// </summary>
        [JsonPropertyName("confirmedAmount")]
        public decimal? ConfirmedAmount { get; set; }

        /// <summary>
        /// Gets or sets the returned amount for the transaction.
        /// </summary>
        [JsonPropertyName("returnedAmount")]
        public decimal? ReturnedAmount { get; set; }

        /// <summary>
        /// Gets or sets the links associated with the payment transaction.
        /// </summary>
        [JsonPropertyName("links")]
        public Link? Links { get; set; }

        /// <summary>
        /// Gets or sets the transaction ID from UFC.
        /// </summary>
        [JsonPropertyName("transactionId")]
        public string? TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the saved card details for recurring payments, including recId, card mask, and expiry date.
        /// If saving a card wasn't required, null will be returned.
        /// </summary>
        [JsonPropertyName("recurringCard")]
        public RecurringCard? RecurringCard { get; set; }
        
        /// <summary>
        /// Gets or sets the payment method used for the transaction.
        /// </summary>
        [JsonPropertyName("paymentMethod")]
        public int? PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the RRN (Transaction Reference Number) for the transaction (12 characters).
        /// </summary>
        [JsonPropertyName("rrn")]
        public string? Rrn { get; set; }

        /// <summary>
        /// Gets or sets an additional parameter for merchant-specific information (optional).
        /// </summary>
        [JsonPropertyName("extra")]
        public string? Extra { get; set; }

        /// <summary>
        /// Gets or sets an additional numeric parameter for merchant-specific information (optional).
        /// </summary>
        [JsonPropertyName("extra2")]
        public decimal? Extra2 { get; set; }
        
        /// <summary>
        /// Gets or sets the preauthorization status for the given payment.
        /// True if preauthorized, otherwise false.
        /// </summary>
        [JsonPropertyName("preAuth")]
        public bool? PreAuth { get; set; }

        /// <summary>
        /// Gets or sets the initiator of the transaction.
        /// The parameter defines by whom the transaction was initiated:
        /// - client: Client initiator transaction
        /// - merchant: Merchant initiator transaction
        /// - none: No recurring payment
        /// </summary>
        [JsonPropertyName("initiator")]
        public string? Initiator { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code for the response.
        /// </summary>
        [JsonPropertyName("httpStatusCode")]
        public int? HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the developer message for logging in the local system.
        /// </summary>
        [JsonPropertyName("developerMessage")]
        public string? DeveloperMessage { get; set; }

        /// <summary>
        /// Gets or sets the user-friendly message that is shown to the user in case of an error or status update.
        /// </summary>
        [JsonPropertyName("userMessage")]
        public string? UserMessage { get; set; }
        
        /// <summary>
        /// Gets or sets the result code, which provides the business-level transaction status description.
        /// </summary>
        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }
        
        /// <summary>
        /// Gets or sets the masked payment card number.
        /// </summary>
        [JsonPropertyName("paymentCardNumber")]
        public string? PaymentCardNumber { get; set; }

        /// <summary>
        /// Gets or sets the operation type for the transaction.
        /// Values:
        /// 0 - Standard QR
        /// 1 - BNPL (Buy Now, Pay Later)
        /// 2 - Ertguli Points
        /// </summary>
        [JsonPropertyName("operationType")]
        public int? OperationType { get; set; }
    }
}
