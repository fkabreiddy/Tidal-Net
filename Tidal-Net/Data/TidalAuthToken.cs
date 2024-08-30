using Newtonsoft.Json;

namespace Tidal_Net.Data;

public class TidalAuthToken
{
    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int? ExpiresIn { get; set; }

    [JsonProperty("token_type")]
    public string? TokenType { get; set; }

    public DateTime ExpiresAt { get; private set; }

    private void SetExpirationDate()
    {

        if (ExpiresIn is not null)
        {
            ExpiresAt = DateTime.Now.AddSeconds((double)ExpiresIn);
        }
        else
        {
            ExpiresAt = DateTime.Now;
            
        }
    }

}