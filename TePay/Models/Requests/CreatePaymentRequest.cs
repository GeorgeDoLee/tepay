using System.Text.Json.Serialization;
using TePay.Models.Requests.Details;

namespace TePay.Models.Requests;

/// <summary>
/// Request model used for creating a payment.
/// </summary>
public class CreatePaymentRequest
{
    /// <summary>
    /// The amount for the payment transaction.
    /// This is a required field and should be an instance of the Amount class,
    /// which contains details about the transaction.
    /// </summary>
    [JsonPropertyName("amount")]
    public required Amount Amount { get; set; }

    /// <summary>
    /// Url to redirect user after finishing payment
    /// </summary>
    [JsonPropertyName("returnUrl")]
    public required string ReturnUrl { get; set; }

    /// <summary>
    /// Additional parameter for merchant specific info (optional). 
    /// only non-unicode (ANSI) symbols allowed. max length 25. 
    /// This parameter will appear in the account statement
    /// </summary>
    [JsonPropertyName("extra")]
    public string? Extra { get; set; }


    /// <summary>
    /// Additional parameter for merchant specific info (optional). 
    /// only non-unicode (ANSI) symbols allowed. max length 52.
    /// </summary>
    [JsonPropertyName("extra2")]
    public string? Extra2 { get; set; }

    /// <summary>
    /// User IP address
    /// </summary>
    [JsonPropertyName("userIpAddress")]
    public string? UserIpAddress { get; set; }


    /// <summary>
    /// Payment initiation expiration time in minutes. Recommended value is max 12 minutes.
    /// If the value is not specified in the request, 12 minutes will be assigned by default.
    /// </summary>
    [JsonPropertyName("expirationMinutes")]
    public int? ExpirationMinutes { get; set; }

    /// <summary>
    /// A list of payment methods (from the PaymentMethod enum) available for the payment.
    /// If no methods are specified, all methods available for the given merchant will be displayed by default.
    /// </summary>
    [JsonPropertyName("methods")]
    public List<PaymentMethod>? Methods { get; set; }

    /// <summary>
    /// list of installment products. mandatory if installment is selected as payment method.
    /// Please note, sum of prices of installment products should be same as total amount.
    /// </summary>
    [JsonPropertyName("installmentProducts")]
    public List<InstallmentProduct>? InstallmentProducts { get; set; }

    /// <summary>
    /// When the payment status changes to final status,
    /// POST request containing PaymentId in the body will be sent to given URL.
    /// </summary>
    [JsonPropertyName("callbackUrl")]
    public string? CallbackUrl { get; set; }

    /// <summary>
    /// Specify if preauthorization is needed for the transaction.
    /// if "true" is passed, amount will be blocked on the card and
    /// additional request should be executed by merchant to complete payment.
    /// </summary>
    [JsonPropertyName("preAuth")]
    public bool? PreAuth { get; set; }

    /// <summary>
    /// Language for payment page.
    /// The following values are allowed: KA, EN
    /// If the language value is not specified in the request, Georgian page will be loaded by default.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// Merchant-side payment identifier
    /// </summary>
    [JsonPropertyName("merchantPaymentId")]
    public string? MerchantPaymentId { get; set; }

    /// <summary>
    /// If true is passed, TBC E-Commerce info message will be skipped and customer will be redirected to merchant.
    /// If false is passed or this parameter isn’t passed at all,
    /// TBC E-Commerce info message will be shown and customer will be redirected to merchant.
    /// </summary>
    [JsonPropertyName("skipInfoMessage")]
    public bool? SkipInfoMessage { get; set; }

    /// <summary>
    /// Specify if saving card funcion is needed.
    /// </summary>
    [JsonPropertyName("saveCard")]
    public bool? SaveCard { get; set; }

    /// <summary>
    /// The date until the card will be saved can be passed in following format "MMYY".
    /// </summary>
    [JsonPropertyName("saveCardToDate")]
    public string? SaveCardToDate { get; set; }

    /// <summary>
    /// Payment short description for clients, max length 30. This parameter will appear on the checkout page
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }      
}