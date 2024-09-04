using System.Text.Json;
using Tidal_Net.Data.Interfaces;
using Tidal_Net.Data.Models;
using Tidal_Net.Data.Utilities;

namespace Tidal_Net.Data.Services;

public class TidalAlbumServices(ITidalRequester requester) : ITidalAlbumServices
{
    private ITidalRequester _requester = requester;
 
   
    public  async Task<List<TidalAlbum>> GetMany(List<string> albumsIds, string token, string market = "us")
    {
       
        var endpoint = new TidalEndpoints(ManyParamsBuilder.Build(albumsIds), market);
        
        _requester.SetToken(token);
        var result = await _requester.Request(endpoint.ManyAlbums);

        if (!result.IsSuccessed())
            return new();
        
        var tidalAlbum = TidalAlbum.CreateMany(result.Json);

        return tidalAlbum ?? new();

            
            

        
       
    }
    
   
    public  async Task<TidalAlbum> GetOne(string albumId, string token, string market = "us")
    {
    
        _requester.SetToken(token);
        var endpoint = new TidalEndpoints(albumId, market);
        var result = await _requester.Request(endpoint.OneAlbum);

        if (!result.IsSuccessed())
            return new();
        
        var tidalAlbum = TidalAlbum.Create(result.Json);
        return tidalAlbum ?? new();

       
      
    }

   
    public  async Task<List<TidalTrack>> GetTracks(string albumId, string token, string market = "us")
    {
        
        var endpoint = new TidalEndpoints(albumId, market);
        
        
        var result = await _requester.Request(endpoint.AlbumTracks);

        if (!result.IsSuccessed())
            return new();
        
        var tracks = TidalAlbum.GetTracks(result.Json);

          
        return tracks ?? new ();
        
    }

    
}