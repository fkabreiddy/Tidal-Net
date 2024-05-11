using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tidal_Net.Authentication
{
    public class Auth
    {
        public static async Task<string> GetAccessTokenAsync(string clientId, string clientSecret)
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
                    dynamic responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    return responseObject.access_token;
                   
                }
                else
                {
                    throw new Exception("Failed to obtain access token. Status code: " + response.StatusCode);
                }
            }

        }
    }
}
