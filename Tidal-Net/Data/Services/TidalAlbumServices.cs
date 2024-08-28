using System.Text.Json;
using Tidal_Net.Data.Models;

namespace Tidal_Net.Data.Services;

public class TidalAlbumServices
{
    public static async Task<TidalResult<List<TidalAlbum>>> GetManyAlbum(List<string> albumsIds, string market = "US")
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
            var tidalApi = new TidalApi(market);
            
            var result = await tidalApi.Request($"albums?countryCode={market.ToUpper()}&include=artists&{parameters}");

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
    public static async Task<TidalResult<TidalAlbum>> GetOne(string albumId, string market = "US")
    {
        try
        {

          
            var tidalApi = new TidalApi(market);
            
            var result = await tidalApi.Request($"albums/{albumId}?countryCode={market.ToUpper()}&include=artists");

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

    
}