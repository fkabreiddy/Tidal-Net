using System.Net.Http.Headers;
using System.Text;
using Tidal_Net.Authentication;
using Tidal_Net.Data.Services;

namespace Tidal_Net.Data;

public class TidalClient
{
    
    
    public TidalAlbumServices Album { get; }
    public TidalTrackServices Tracks { get; }
    public TidalArtistServices Artist { get; }

    private static HttpClient _httpClient;
    
    private readonly TidalRequester _requester;

    public TidalClient( string token, string market = "us")
    {
       
        BuildHttpClient(token);
        _requester = new TidalRequester(_httpClient);
        Album = new TidalAlbumServices(_requester, market);
        Tracks = new TidalTrackServices(_requester, market);
        Artist = new TidalArtistServices(_requester, market);
    }
  

    private void BuildHttpClient(string token)
    {
        
        _httpClient = new HttpClient() ?? throw new ArgumentNullException(nameof(_httpClient));
        _httpClient.BaseAddress = new Uri("https://openapi.tidal.com/v2/");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.tidal.v1+json"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    }
    
    
    
  
}