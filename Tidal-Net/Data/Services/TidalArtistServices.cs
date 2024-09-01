using Tidal_Net.Data.Models;
using Tidal_Net.Data.Utilities;

namespace Tidal_Net.Data.Services;

public class TidalArtistServices(TidalRequester requester, string market = "US") 
{
    private TidalRequester _requester = requester;

    public async Task<TidalArtist> GetOne(string artistId)
    {
        
        var endpoint = new TidalEndpoints(artistId, market);
        var result = await _requester.Request(endpoint.OneArtist);

        if (!result.IsSuccessed())
            return new();

        var artists = TidalArtist.CreateOne(result.Json);
        return artists ?? new();

       
    }
    public async Task<List<TidalArtist>> GetMany(List<string> artistIds)
    {
        
        var endpoint = new TidalEndpoints(ManyParamsBuilder.Build(artistIds), market);
        
        var result = await _requester.Request(endpoint.ManyArtists);
        
        if (!result.IsSuccessed())
            return new();
        
        var artists = TidalArtist.CreateMany(result.Json);
        
        return  artists ?? new();

       
    }
}