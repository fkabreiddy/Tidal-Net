namespace Tidal_Net.Data.Interfaces;

public interface ITidalTokenBuilder
{
    /// <summary>
    /// Generates an access token using the provided client credentials.
    /// </summary>
    /// <param name="clientId">The client ID for authentication.</param>
    /// <param name="clientSecret">The client secret for authentication.</param>
    /// <returns>
    /// A `Task` representing the asynchronous operation. The task result contains the access token as a string,
    /// or `null` if the token could not be generated.
    /// </returns>
    /// <exception cref="Exception">Thrown when the request fails or when something goes wrong during processing.</exception>
    Task<string?> Build(string clientId, string clientSecret);
}