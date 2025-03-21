namespace TePay.Interfaces;

public interface ITePayAuthenticator
{
    /// <summary>
    /// Authenticates the user and retrieves an access token if needed.
    /// </summary>
    Task AuthenticateAsync();
}