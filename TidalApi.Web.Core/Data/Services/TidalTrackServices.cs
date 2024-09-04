using TidalApi.Web.Core.Data.Interfaces;
using TidalApi.Web.Core.Data.Models;
using TidalApi.Web.Core.Data.Utilities;

namespace TidalApi.Web.Core.Data.Services;

public class TidalTrackServices(ITidalRequester requester) : ITidalTrackServices
{
    private ITidalRequester _requester = requester;

    
    /// <summary>
    /// Retrieves information about a specific Tidal track.
    /// </summary>
    /// <param name="trackId">The ID of the track to retrieve information for.</param>
    /// <param name="token">The authentication token used to access the Tidal API.</param>
    /// <param name="market">The market to be used for the request, with a default value of "US".</param>
    /// <returns>A <see cref="TidalTrack"/> object containing information about the specified track. Returns a new, empty instance if the request is unsuccessful.</returns>
    /// <remarks>
    /// This method sends a request to the Tidal API to retrieve information about a specific track.
    /// If the request is not successful, an empty track object is returned.
    /// </remarks>
    public async Task<TidalTrack> GetOne(string trackId, string token, string market = "US")
    {
        
        var endpoint = new TidalEndpoints(trackId, market);
        _requester.SetToken(token);
        var result = await _requester.Request(endpoint.OneTrack);

        if (!result.IsSuccessed())
            return new();

        var tracks = TidalTrack.CreateOne(result.Json);

        return tracks ?? new();

       
    }
    
    /// <summary>
    /// Retrieves information about multiple Tidal tracks based on the provided track IDs.
    /// </summary>
    /// <param name="trackIds">A list of track IDs to retrieve information for.</param>
    /// <param name="token">The authentication token used to access the Tidal API.</param>
    /// <param name="market">The market to be used for the request, with a default value of "US".</param>
    /// <returns>A list of <see cref="TidalTrack"/> objects corresponding to the provided track IDs. Returns an empty list if the request is unsuccessful.</returns>
    /// <remarks>
    /// This method sends a request to the Tidal API to retrieve information for multiple tracks.
    /// If the request is not successful, an empty list is returned.
    /// </remarks>
    
    public async Task<List<TidalTrack>> GetManyTracks(List<string> trackIds, string token, string market = "US")
    {
         
        var endpoint  = new TidalEndpoints(ManyParamsBuilder.Build(trackIds), market);
        _requester.SetToken(token);
        var result = await _requester.Request(endpoint.ManyTracks);

        if (!result.IsSuccessed())
            return new();

        var tracks = TidalTrack.CreateMany(result.Json);

        return tracks ?? new();
        
    }

    
}