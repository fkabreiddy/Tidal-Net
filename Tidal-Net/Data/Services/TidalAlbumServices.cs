using System.Text.Json;
using Tidal_Net.Data.Models;

namespace Tidal_Net.Data.Services;

public class TidalAlbumServices(TidalRequester requester, string market = "US")
{
    private TidalRequester _requester = requester;
    public  async Task<TidalResult<List<TidalAlbum>>> GetMany(List<string> albumsIds)
    {
        try
        {
            var parameters = string.Empty;

            foreach (var id in albumsIds)
            {
                if (id == albumsIds.Last())
                {
                    parameters += $"filter%5Bid%5D={id}";
                }
                else
                {
                    parameters += $"filter%5Bid%5D={id}&";
                }
               
            }
           
            
            var result = await _requester.Request($"albums?countryCode={market.ToUpper()}&include=artists&{parameters}");

            if (result.Success && result.Data is string data)
            {
                var tidalAlbum = TidalAlbum.CreateMany(data);

                if (tidalAlbum is not null)
                {
                    return new("Operation successfull",tidalAlbum, true);
                }
                
                

            }
           
            return new(result.Message);
            
            
            

        }
        catch(Exception ex)
        {
            return new(ex.InnerException?.Message ?? ex.Message);
        }
    }
    public  async Task<TidalResult<TidalAlbum>> GetOne(string albumId)
    {
        try
        {

          
           
            
            var result = await _requester.Request($"albums/{albumId}?countryCode={market.ToUpper()}&include=artists");

            if (result.Success && result.Data is string data)
            {
                var tidalAlbum = TidalAlbum.Create(data);

                if (tidalAlbum is not null)
                {
                    return new("Operation successfull",tidalAlbum, true);
                }
                
                

            }
           
            return new(result.Message);
            
            
            

        }
        catch(Exception ex)
        {
            return new(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public  async Task<TidalResult<List<TidalTrack>>> GetTracks(string albumId)
    {
        try
        {

          
           
            
            var result = await _requester.Request($"albums/{albumId}?countryCode={market.ToUpper()}&include=items&include=artists");

            if (result.Success && result.Data is string data)
            {
                var tracks = TidalAlbum.GetTracks(data);

              
                    return new("Operation successfull",tracks, true);
                

            }
           
            return new(result.Message);
            
            
            

        }
        catch(Exception ex)
        {
            return new(ex.InnerException?.Message ?? ex.Message);
        }
    }

    
}