using System.Net.Http.Headers;
using System.Text;
using TidalApi.Web.Core.Data.Interfaces;

namespace TidalApi.Web.Core.Authentication
{
    public class TidalTokenBuilder : ITidalTokenBuilder
    {

        private readonly HttpClient _httpClient;
        public TidalTokenBuilder(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("TidalAuthClient");
        }
        public async Task<string?> Build(string clientId, string clientSecret)
        {

            try
            {
                
                string base64Creds = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));


                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Creds);
                var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                HttpResponseMessage response = await _httpClient.PostAsync("token", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseBody);

                    if (responseObject is null || string.IsNullOrEmpty(responseObject.access_token.ToString()))
                        return response.ReasonPhrase ?? "There was a problem requesting the token. Check the credentials provided.";
                        
                    return responseObject?.access_token.ToString();

                }
                else
                {
                    throw new Exception("Fail. " + response.StatusCode);
                }
            }
            catch(Exception ex)
            {
                
                throw new Exception("Something went wrong " + ex.InnerException?.Message ?? ex.Message);

            }
            
        }

        

        
    }
}
