
using System.Net.Http.Headers;
using Tidal_Net.Authentication;
using Tidal_Net.Data.Services;

namespace Tidal_Net.Data
{
    public  class TidalRequester (HttpClient client)
    {
        private readonly HttpClient _httpClient = client;
        public  async Task<TidalResponse> Request(string endPoint)
        {
            try
            {


                HttpResponseMessage response = await _httpClient.GetAsync(endPoint);

                if (response.IsSuccessStatusCode)
                {
                    // Read the content of the response
                    var value = await response.Content.ReadAsStringAsync();
                    return new("Operation Successful", value, response.IsSuccessStatusCode);
                }
                else
                {
                    // Handle non-success status codes if needed
                    return new("Operation Failed", string.Empty, response.IsSuccessStatusCode);
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                // Handle HTTP request-specific exceptions
                // Consider logging or handling the exception
                return new("Request Failed", httpRequestException.Message, false);
            }
            catch (Exception e)
            {
                // Handle general exceptions
                // Consider logging or handling the exception
                return new("An Error Occurred", e.Message, false);
            }
        }
        
        
      

        
    }
    
    
}
