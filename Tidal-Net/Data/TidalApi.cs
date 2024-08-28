
using System.Net.Http.Headers;
using Tidal_Net.Authentication;

namespace Tidal_Net.Data
{
    internal  class TidalApi(string market = "US")
    {
        private const string BaseUrl = "https://openapi.tidal.com/v2/";
        private  readonly Auth _auth = new();
        public async Task<TidalResult<object>> Request(string endPoint)
        {
            try
            {

                if (_auth.GetMyToken() is null)
                    return new("Initialize the auth class to get the access token from Tidal's api");

             
                
                var apiUrl = $"{BaseUrl}{endPoint}";

                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.tidal.v1+json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.GetMyToken());

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
