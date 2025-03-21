namespace TePay.Configuration;

/// <summary>
/// Represents the configuration settings required for the TBC Payment API integration.
/// Contains API authentication details, base URL, and version for the TBC Payment API.
/// </summary>
public class TePayConfig
{
    /// <summary>
    /// Gets or sets the API key used for authenticating API requests.
    /// </summary>
    public required string ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the client ID associated with the TBC Payment API.
    /// </summary>
    public required string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the client secret associated with the TBC Payment API.
    /// </summary>
    public required string ClientSecret { get; set; }

    /// <summary>
    /// Gets or sets the base URL of the TBC Payment API. 
    /// Default is set to "https://api.tbcbank.ge".
    /// </summary>
    public Uri BaseUrl { get; set; } = new Uri("https://api.tbcbank.ge");

    /// <summary>
    /// Gets or sets the version of the TBC Payment API to be used.
    /// Default is set to "v1".
    /// </summary>
    public string Version { get; set; } = "v1";
}
