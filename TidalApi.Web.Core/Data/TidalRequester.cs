using System.Net.Http.Headers;
using TidalApi.Web.Core.Data.Interfaces;

namespace TidalApi.Web.Core.Data
{
    public  class TidalRequester : ITidalRequester
    {
        private readonly HttpClient _httpClient;
        private string? Token { get; set; } 

        public TidalRequester(IHttpClientFactory clientFactory)
        {
            this._httpClient = clientFactory.CreateClient("TidalApiClient");
        }
      
        public  async Task<TidalResponse> Request(string endPoint)
        {
            try
            {

                
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
                    Token ?? throw new ArgumentNullException(nameof(Token)) );

                HttpResponseMessage response = await _httpClient.GetAsync(endPoint);

                if (response.IsSuccessStatusCode)
                {
                    // Read the content of the response
                    var value = await response.Content.ReadAsStringAsync();
                    return new
                    (
                        response.ReasonPhrase ?? "Success", 
                        value, 
                        response.IsSuccessStatusCode
                    );
                }
                else
                {
                    // Handle non-success status codes if needed
                    return new(response.ReasonPhrase ?? "Operation Failed", 
                        string.Empty, 
                        response.IsSuccessStatusCode
                    );
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                // Handle HTTP request-specific exceptions
                // Consider logging or handling the exception
                return new(httpRequestException.Message, string.Empty, false);
            }
            catch (Exception e)
            {
                // Handle general exceptions
                // Consider logging or handling the exception
                return new(e.InnerException?.Message ?? e.Message, string.Empty, false);
            }
        }

        public void SetToken(string token)
        {
            this.Token = token;
        }
        
        
      

        
    }
    
    
}
