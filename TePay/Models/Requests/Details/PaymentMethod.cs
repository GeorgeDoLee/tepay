namespace TePay.Models.Requests.Details;

/// <summary>
/// Enum representing the various payment methods available when initiating TBC E-Commerce payments.
/// If no payment method is passed, by default, all methods available for the given merchant will be displayed.
/// The passed items must be unique.
/// </summary>
public enum PaymentMethod
{
    /// <summary>
    /// Web QR payment method (BNPL - Buy Now, Pay Later).
    /// Service must be activated for the merchant from the back-office. 
    /// </summary>
    WebQR = 4,

    /// <summary>
    /// Payment with card (PAN).
    /// This method is activated by default.
    /// </summary>
    Pan = 5,

    /// <summary>
    /// Payment via Internet Bank Login.
    /// Service must be activated for the merchant from the back-office. 
    /// </summary>
    InternetBankLogin = 7,

    /// <summary>
    /// Installment payment method.
    /// To activate Installment payments, configure the Installment campaignID and MerchantKey parameters 
    /// from the merchant dashboard. To register as an Installments merchant, consult the relevant instructions.
    /// </summary>
    Installment = 8,

    /// <summary>
    /// Apple Pay payment method.
    /// Service must be activated for the merchant from the back-office. 
    /// </summary>
    ApplePay = 9,

    /// <summary>
    /// Google Pay payment method.
    /// Service must be activated for the merchant from the back-office. 
    /// </summary>
    GooglePay = 14
}
