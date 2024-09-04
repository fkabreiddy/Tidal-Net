using Tidal_Net.Data.Interfaces;
using Tidal_Net.Data.Models;
using Tidal_Net.Data.Utilities;

namespace Tidal_Net.Data.Services;

public class TidalArtistServices(ITidalRequester requester) : ITidalArtistServices
{
    private ITidalRequester _requester = requester;

    public async Task<TidalArtist> GetOne(string artistId, string token, string market = "US")
    {
        
        var endpoint = new TidalEndpoints(artistId, market);
        _requester.SetToken(token);
        var result = await _requester.Request(endpoint.OneArtist);

        if (!result.IsSuccessed())
            return new();

        var artists = TidalArtist.CreateOne(result.Json);
        return artists ?? new();

       
    }
    public async Task<List<TidalArtist>> GetMany(List<string> artistIds, string token, string market = "US")
    {
        
        
        var endpoint = new TidalEndpoints(ManyParamsBuilder.Build(artistIds), market);
        _requester.SetToken(token);
        var result = await _requester.Request(endpoint.ManyArtists);
        
        if (!result.IsSuccessed())
            return new();
        
        var artists = TidalArtist.CreateMany(result.Json);
        
        return  artists ?? new();

       
    }
}