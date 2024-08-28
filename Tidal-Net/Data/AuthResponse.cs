using Newtonsoft.Json;

namespace Tidal_Net.Data;

public class AuthResponse
{
    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int? ExpiresIn { get; set; }

    [JsonProperty("token_type")]
    public string? TokenType { get; set; }
    
   
}