using System.Text.Json.Serialization;

namespace TePay.Models.Responses;

/// <summary>
/// Represents the response containing the access token and related details.
/// This class is used for storing the authentication token information
/// returned by an authentication service.
/// </summary>
public class AccessTokenResponse
{
    /// <summary>
    /// Gets or sets the access token.
    /// The access token is used for authenticating subsequent API requests.
    /// </summary>
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the access token in seconds.
    /// This value indicates how long the token is valid before it expires.
    /// </summary>
    [JsonPropertyName("expires_in")]
    public required int ExpiresIn { get; set; }

    /// <summary>
    /// Gets or sets the type of the token.
    /// The token type is usually set to "Bearer".
    /// </summary>
    [JsonPropertyName("token_type")]
    public required string TokenType { get; set; }
}