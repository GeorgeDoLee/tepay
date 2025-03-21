using Serilog;
using System.Net.Http.Headers;
using TePay.Configuration;
using TePay.Exceptions;
using TePay.Helpers;
using TePay.Interfaces;
using TePay.Models.Responses;

namespace TePay.Services;

/// <summary>
/// Handles authentication for TBC Pay by obtaining and managing access tokens.
/// This class is responsible for retrieving, storing, and refreshing authentication tokens 
/// required for API requests.
/// </summary>
internal class TePayAuthenticator : ITePayAuthenticator
{
    private readonly TePayConfig _config;
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    private string? _accessToken;
    private DateTime? _accessTokenExpirationDate;

    /// <summary>
    /// Initializes a new instance of the <see cref="TePayAuthenticator"/> class.
    /// </summary>
    /// <param name="config">The configuration settings required for authentication.</param>
    /// <param name="httpClient">The HTTP client used for sending authentication requests.</param>
    /// <param name="loggerConfiguration">
    /// Optional custom logger configuration.
    /// </param>
    public TePayAuthenticator(TePayConfig config, HttpClient httpClient, LoggerConfiguration? loggerConfiguration = null)
    {
        _config = config;
        _httpClient = httpClient;
        _logger = LoggerHelper.CreateLogger<TePayAuthenticator>(loggerConfiguration);
    }

    
    /// <inheritdoc />
    /// <exception cref="TbcPayAuthenticationException">Thrown if the authentication fails.</exception>
    /// <exception cref="TbcPayApiException">Thrown if the API response indicates a failure (non-success status code).</exception>
    /// <exception cref="TbcPaySerializationException">Thrown if there is an error during serialization or deserialization of the request or response content.</exception>
    /// <exception cref="Exception">Thrown for other unexpected errors.</exception>
    public async Task AuthenticateAsync()
    {
        _logger.Information("Starting authentication...");

        if (IsAccessTokenValid())
        {
            _logger.Information("Using existing access token.");
            return;
        }

        await GetNewAccessTokenAsync();
    }

    /// <summary>
    /// Checks if the current access token is valid.
    /// </summary>
    /// <returns>True if the token is valid, false otherwise.</returns>
    private bool IsAccessTokenValid()
    {
        return !string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _accessTokenExpirationDate;
    }

    /// <summary>
    /// Sends a request to obtain a new access token if the current one is expired or unavailable.
    /// </summary>
    /// <exception cref="TbcPayAuthenticationException">Thrown if the authentication fails.</exception>
    /// <exception cref="TbcPayApiException">Thrown if the API response indicates a failure (non-success status code).</exception>
    /// <exception cref="TbcPaySerializationException">Thrown if there is an error during serialization or deserialization of the request or response content.</exception>
    /// <exception cref="Exception">Thrown for other unexpected errors.</exception>
    private async Task GetNewAccessTokenAsync()
    {
        _logger.Information("Sending request to obtain a new access token.");

        var request = new HttpRequestMessage(HttpMethod.Post, "tpay/access-token")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = _config.ClientId,
                ["client_secret"] = _config.ClientSecret
            })
        };

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            _logger.Error("Authentication failed. Status Code: {StatusCode}", response.StatusCode);
            await HandleErrorResponseAsync(response);
            return;
        }

        await SetAccessTokenAsync(response);
    }

    /// <summary>
    /// Handles error responses during the authentication process.
    /// </summary>
    /// <param name="response">The HTTP response from the authentication request.</param>
    /// <exception cref="TbcPayAuthenticationException">Thrown if the authentication fails.</exception>
    /// <exception cref="TbcPaySerializationException">Thrown if there is an error during serialization or deserialization of the request or response content.</exception>
    /// <exception cref="Exception">Thrown for other unexpected errors.</exception>
    private async Task HandleErrorResponseAsync(HttpResponseMessage response)
    {
        using var errorContent = await response.Content.ReadAsStreamAsync();
        ErrorResponse errorResponse = await JsonHelper.DeserializeAsync<ErrorResponse>(errorContent);

        throw new TePayAuthenticationException(response.StatusCode, errorResponse);
    }

    /// <summary>
    /// Extracts the access token from the response and sets the expiration date.
    /// </summary>
    /// <param name="response">The HTTP response containing the access token.</param>
    /// <exception cref="Exception">Thrown for other unexpected errors.</exception>
    private async Task SetAccessTokenAsync(HttpResponseMessage response)
    {
        _logger.Information("Authentication successful, retrieving token.");

        using var stream = await response.Content.ReadAsStreamAsync();
        var tokenResponse = await JsonHelper.DeserializeAsync<AccessTokenResponse>(stream);

        _accessToken = tokenResponse.AccessToken;
        _accessTokenExpirationDate = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 60);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

        _logger.Information("Access token successfully retrieved and added to HTTP headers.");
    }
}