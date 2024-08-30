
using System.Net.Http.Headers;
using Tidal_Net.Authentication;
using Tidal_Net.Data.Services;

namespace Tidal_Net.Data
{
    public  class TidalRequester(TidalAuthToken token)
    {
        private const string BaseUrl = "https://openapi.tidal.com/v2/";

        public TidalAuthToken _token = token;
       
        public  async Task<TidalResult<object>> Request(string endPoint)
        {
            try
            {


                if (token.ExpiresAt >= DateTime.Now)
                    return new("The token is already expired.");
                
                var apiUrl = $"{BaseUrl}{endPoint}";

                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.tidal.v1+json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.AccessToken);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido de la respuesta
                    var value = await response.Content.ReadAsStringAsync();

                    return new("Operation Successful", value, true);

                }
                else
                {
                    // Manejar el caso de error
                    return new($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception e)
            {
                return new(e.InnerException?.Message ?? e.Message);
            }
        }
      

        
    }
    
    
}
