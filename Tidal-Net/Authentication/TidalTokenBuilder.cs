
using System.Net.Http.Headers;
using System.Text;
using Tidal_Net.Data;

namespace Tidal_Net.Authentication
{
    public class TidalTokenBuilder
    {
        public static  TidalAuthToken? Token { get; private set; }
     
        public static  async Task<TidalAuthToken?> Build(string clientId, string clientSecret)
        {

            try
            {
                const string authUrl = "https://auth.tidal.com/v1/oauth2/token";
                string base64Creds = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));


                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Creds);
                    var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await client.PostAsync(authUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<TidalAuthToken>(responseBody);

                        if (responseObject is null || string.IsNullOrEmpty(responseObject.AccessToken))
                            return null;

                        Token = responseObject;


                        return Token;

                    }
                    else
                    {
                        throw new Exception("Failed to obtain access token. Status code: " + response.StatusCode);
                    }
                }

            }
            catch(Exception ex)
            {
                
                throw new Exception("Failed to obtain access token. Status code: " + ex.Message);

            }
            
        }

        public string? GetMyToken() => Token?.AccessToken;

        
    }
}
