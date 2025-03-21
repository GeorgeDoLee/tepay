using Serilog;
using TePay.Configuration;
using TePay.ErrorHandling;
using TePay.Exceptions;
using TePay.Helpers;
using TePay.Interfaces;
using TePay.Models.Requests;
using TePay.Models.Responses;
using TePay.Validators;

namespace TePay.Services;

/// <summary>
/// Provides implementation for TBC payment operations. This service interacts with the TBC Payment API.
/// </summary>
public class TePayService : ITePayService
{
    private readonly ITePayApiClient _apiClient;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TePayService"/> class.
    /// </summary>
    /// <param name="config">The configuration settings for the TBC Payment API.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="config"/> is null.</exception>
    public TePayService(TePayConfig config, LoggerConfiguration? loggerConfiguration = null)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config), "config can't be null.");
        }

        _logger = LoggerHelper.CreateLogger<TePayService>(loggerConfiguration);

        _apiClient = new TePayApiClient(config, loggerConfiguration);
    }

    /// <inheritdoc />
    /// <exception cref="TePayAuthenticationException">Thrown if the authentication fails.</exception>
    /// <exception cref="TePayValidationException">Thrown if the request validation fails.</exception>
    /// <exception cref="TePayApiException">Thrown if an error occurs while calling the TBC Payment API.</exception>
    /// <exception cref="TePaySerializationException">Thrown if serialization or deserialization fails.</exception>
    /// <exception cref="Exception">Thrown if an unexpected error occurs.</exception>
    public async Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest request)
    {
        return await ErrorHandler.HandleAsync(async () =>
        {
            _logger.Information("Validating {RequestName}", request.GetType().Name);
            ValidatorHelper.Validate(request, new CreatePaymentRequestValidator());

            _logger.Information("Creating payment with request: {@Request}", request);
            var response = await _apiClient.SendRequestAsync<CreatePaymentResponse>(HttpMethod.Post, "tpay/payments", request);
            
            _logger.Information("Payment created successfully. Response: {@Response}", response);
            return response;
        }, _logger);
    }

    /// <inheritdoc />
    /// <exception cref="TePayAuthenticationException">Thrown if the authentication fails.</exception>
    /// <exception cref="TePayApiException">Thrown if an error occurs while fetching payment details.</exception>
    /// <exception cref="TePaySerializationException">Thrown if serialization or deserialization fails.</exception>
    /// <exception cref="Exception">Thrown if an unexpected error occurs.</exception>
    public async Task<PaymentDetailsResponse> GetPaymentDetailsAsync(string payId)
    {
        return await ErrorHandler.HandleAsync(async () =>
        {
            _logger.Information("Fetching payment details for PayId: {PayId}", payId);
            var response = await _apiClient.SendRequestAsync<PaymentDetailsResponse>(HttpMethod.Get, $"tpay/payments/{payId}");
            
            _logger.Information("Fetched payment details: {@Response}", response);
            return response;
        }, _logger);
    }

    /// <inheritdoc />
    /// <exception cref="TePayAuthenticationException">Thrown if the authentication fails.</exception>
    /// <exception cref="TePayValidationException">Thrown if the request validation fails.</exception>
    /// <exception cref="TePayApiException">Thrown if an error occurs while fetching payment details.</exception>
    /// <exception cref="TePaySerializationException">Thrown if serialization or deserialization fails.</exception>
    /// <exception cref="Exception">Thrown if an unexpected error occurs.</exception>
    public async Task CancelPaymentAsync(string payId, CancelPaymentRequest request)
    {
        await ErrorHandler.HandleAsync(async () =>
        {
            _logger.Information("Validating {RequestName}", request.GetType().Name);
            ValidatorHelper.Validate(request, new CancelPaymentRequestValidator());
            
            _logger.Information("Cancelling payment for PayId: {PayId} with request: {@Request}", payId, request);
            await _apiClient.SendRequestAsync(HttpMethod.Post, $"tpay/payments/{payId}/cancel", request);
            
            _logger.Information("Payment cancelled successfully for PayId: {PayId}", payId);
        }, _logger);
    }

    /// <inheritdoc />
    /// <exception cref="TePayAuthenticationException">Thrown if the authentication fails.</exception>
    /// <exception cref="TePayApiException">Thrown if an error occurs while fetching payment details.</exception>
    /// <exception cref="TePaySerializationException">Thrown if serialization or deserialization fails.</exception>
    /// <exception cref="Exception">Thrown if an unexpected error occurs.</exception>
    public async Task<CompletePreAuthPaymentResponse> CompletePreAuthPaymentAsync(string payId, decimal amount)
    {
        return await ErrorHandler.HandleAsync(async () =>
        {
            _logger.Information("Completing pre-auth payment for PayId: {PayId} with amount: {Amount}", payId, amount);
            var response = await _apiClient.SendRequestAsync<CompletePreAuthPaymentResponse>(
                HttpMethod.Post, $"tpay/payments/{payId}/completion", new { amount });

            _logger.Information("Pre-auth payment completed successfully. Response: {@Response}", response);
            return response;
        }, _logger);
    }

    /// <inheritdoc />
    /// <exception cref="TePayAuthenticationException">Thrown if the authentication fails.</exception>
    /// <exception cref="TePayValidationException">Thrown if the request validation fails.</exception>
    /// <exception cref="TePayApiException">Thrown if an error occurs while fetching payment details.</exception>
    /// <exception cref="TePaySerializationException">Thrown if serialization or deserialization fails.</exception>
    /// <exception cref="Exception">Thrown if an unexpected error occurs.</exception>
    public async Task ExecuteRecurringPaymentAsync(ExecuteRecurringPaymentRequest request)
    {
        await ErrorHandler.HandleAsync(async () =>
        {
            _logger.Information("Validating {RequestName}", request.GetType().Name);
            ValidatorHelper.Validate(request, new ExecuteRecurringPaymentRequestValidator());

            _logger.Information("Executing recurring payment with request: {@Request}", request);
            await _apiClient.SendRequestAsync(HttpMethod.Post, "tpay/payments/execution", request);

            _logger.Information("Recurring payment executed successfully.");
        }, _logger);
    }

    /// <inheritdoc />
    /// <exception cref="TePayAuthenticationException">Thrown if the authentication fails.</exception>
    /// <exception cref="TePayApiException">Thrown if an error occurs while fetching payment details.</exception>
    /// <exception cref="TePaySerializationException">Thrown if serialization or deserialization fails.</exception>
    /// <exception cref="Exception">Thrown if an unexpected error occurs.</exception>
    public async Task DeleteRecurringPaymentAsync(string recId)
    {
        await ErrorHandler.HandleAsync(async () =>
        {
            _logger.Information("Deleting recurring payment with RecId: {RecId}", recId);
            await _apiClient.SendRequestAsync(HttpMethod.Delete, $"tpay/payments/{recId}");

            _logger.Information("Recurring payment deleted successfully for RecId: {RecId}", recId);
        }, _logger);
    }
}
