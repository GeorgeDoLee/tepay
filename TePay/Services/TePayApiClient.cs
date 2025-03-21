using Serilog;
using System.Text;
using TePay.Configuration;
using TePay.Exceptions;
using TePay.Helpers;
using TePay.Interfaces;
using TePay.Models.Responses;

namespace TePay.Services;

/// <summary>
/// Provides implementation for TBC payment API client.
/// </summary>
internal class TePayApiClient : ITePayApiClient
{
    private readonly HttpClient _httpClient;
    private readonly TePayConfig _config;
    private readonly ITePayAuthenticator _authenticator;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TePayApiClient"/> class.
    /// Sets up the HTTP client, configures API key, and prepares the authenticator.
    /// </summary>
    /// <param name="config">The TBC Pay configuration settings.</param>
    public TePayApiClient(TePayConfig config, LoggerConfiguration? loggerConfiguration = null)
    {

        _config = config;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(_config.BaseUrl, _config.Version + "/")
        };
        _httpClient.DefaultRequestHeaders.Add("apiKey", _config.ApiKey);
        _logger = LoggerHelper.CreateLogger<TePayApiClient>(loggerConfiguration);
        _authenticator = new TePayAuthenticator(_config, _httpClient, loggerConfiguration);
    }

    /// <inheritdoc />
    /// <exception cref="TePayAuthenticationException">Thrown if the authentication fails.</exception>
    /// <exception cref="TePayApiException">Thrown if the API response indicates a failure (non-success status code).</exception>
    /// <exception cref="TePaySerializationException">Thrown if there is an error during serialization or deserialization of the request or response content.</exception>
    /// <exception cref="Exception">Thrown for other unexpected errors.</exception>
    public async Task<T> SendRequestAsync<T>(HttpMethod method, string endpoint, object content = null!)
    {
        _logger.Information("Preparing to send request to {Endpoint} with method {Method}. Content: {@Content}", endpoint, method, content);
        await _authenticator.AuthenticateAsync();

        using var response = await SendCoreRequestAsync(method, endpoint, content);
        _logger.Information("Received response from {Endpoint} with status code {StatusCode}", endpoint, response.StatusCode);

        await using var stream = await response.Content.ReadAsStreamAsync();
        return await JsonHelper.DeserializeAsync<T>(stream);
    }

    /// <inheritdoc />
    /// <exception cref="TePayAuthenticationException">Thrown if the authentication fails.</exception>
    /// <exception cref="TePayApiException">Thrown if the API response indicates a failure (non-success status code).</exception>
    /// <exception cref="TePaySerializationException">Thrown if there is an error during serialization of the request content.</exception>
    /// <exception cref="Exception">Thrown for other unexpected errors.</exception>
    public async Task SendRequestAsync(HttpMethod method, string endpoint, object content = null!)
    {
        _logger.Information("Preparing to send request to {Endpoint} with method {Method}. Content: {@Content}", endpoint, method, content);

        await _authenticator.AuthenticateAsync();
        using var response = await SendCoreRequestAsync(method, endpoint, content);

        _logger.Information("Received response from {Endpoint} with status code {StatusCode}", endpoint, response.StatusCode);
    }

    /// <summary>
    /// Sends the core HTTP request to the API endpoint and returns the response.
    /// Handles request serialization and error checking.
    /// </summary>
    /// <param name="method">The HTTP method to use (GET, POST, etc.).</param>
    /// <param name="endpoint">The endpoint for the request.</param>
    /// <param name="content">The content of the request, serialized to JSON.</param>
    /// <returns>A task representing the asynchronous operation, returning the HTTP response.</returns>
    /// <exception cref="TePayApiException">Thrown if the API response indicates a failure (non-success status code).</exception>
    /// <exception cref="TePaySerializationException">Thrown if there is an error during serialization or deserialization of the request or response content.</exception>
    /// <exception cref="Exception">Thrown for other unexpected errors.</exception>
    private async Task<HttpResponseMessage> SendCoreRequestAsync(HttpMethod method, string endpoint, object content)
    {
        var request = new HttpRequestMessage(method, endpoint);

        if (content != null)
        {
            var json = JsonHelper.Serialize(content);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            _logger.Information("Request content serialized: {Content}", json);
        }

        _logger.Information("Sending request to {Endpoint} with method {Method}", endpoint, method);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            _logger.Error("Request to {Endpoint} failed with status code: {StatusCode}", endpoint, response.StatusCode);

            using var errorContent = await response.Content.ReadAsStreamAsync();
            ErrorResponse errorResponse = await JsonHelper.DeserializeAsync<ErrorResponse>(errorContent);

            throw new TePayApiException(response.StatusCode, errorResponse);
        }

        return response;
    }
}
