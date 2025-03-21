using TePay.Models.Requests;
using TePay.Models.Responses;

namespace TePay.Interfaces;

/// <summary>
/// Defines operations for interacting with the TBC Payment service
/// </summary>
public interface ITePayService
{
    /// <summary>
    /// Initiates a new TBC E-Commerce payment.
    /// </summary>
    /// <param name="request">The request containing payment details.</param>
    /// <returns>A task that represents the asynchronous operation, returning the created payment response.</returns>
    Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest request);

    /// <summary>
    /// Retrieves the details of a payment by its payment ID.
    /// </summary>
    /// <param name="payId">The unique identifier of the payment.</param>
    /// <returns>A task that represents the asynchronous operation, returning payment details.</returns>
    Task<PaymentDetailsResponse> GetPaymentDetailsAsync(string payId);

    /// <summary>
    /// Cancels an existing payment.
    /// </summary>
    /// <param name="payId">The unique identifier of the payment to cancel.</param>
    /// <param name="request">The request containing cancellation details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CancelPaymentAsync(string payId, CancelPaymentRequest request);

    /// <summary>
    /// Completes a pre-authorized payment by capturing the blocked amount.
    /// </summary>
    /// <param name="payId">The unique identifier of the payment.</param>
    /// <param name="amount">The amount to be captured (should not exceed the blocked amount).</param>
    /// <returns>A task that represents the asynchronous operation, returning the completion response.</returns>
    Task<CompletePreAuthPaymentResponse> CompletePreAuthPaymentAsync(string payId, decimal amount);

    /// <summary>
    /// Initiates a recurring payment.
    /// </summary>
    /// <param name="request">The request containing recurring payment details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ExecuteRecurringPaymentAsync(ExecuteRecurringPaymentRequest request);

    /// <summary>
    /// Deletes a recurring payment based on the given recurring payment ID.
    /// </summary>
    /// <param name="recId">The unique identifier of the recurring payment to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteRecurringPaymentAsync(string recId);
}
