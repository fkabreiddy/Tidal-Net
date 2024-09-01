using Tidal_Net.Data.Models;
using Tidal_Net.Data.Utilities;

namespace Tidal_Net.Data.Services;

public class TidalTrackServices(TidalRequester requester, string market = "US") 
{
    private TidalRequester _requester = requester;

    public async Task<TidalTrack> GetOne(string trackId)
    {
        
        var endpoint = new TidalEndpoints(trackId, market);
        var result = await _requester.Request(endpoint.OneTrack);

        if (!result.IsSuccessed())
            return new();

        var tracks = TidalTrack.CreateOne(result.Json);

        return tracks ?? new();

       
    }
    
    public async Task<List<TidalTrack>> GetManyTracks(List<string> trackIds)
    {
         
        var endpoint  = new TidalEndpoints(ManyParamsBuilder.Build(trackIds), market);
        
        var result = await _requester.Request(endpoint.ManyTracks);

        if (!result.IsSuccessed())
            return new();

        var tracks = TidalTrack.CreateMany(result.Json);

        return tracks ?? new();
        
    }

    
}