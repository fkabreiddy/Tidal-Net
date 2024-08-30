using Tidal_Net.Data.Models;

namespace Tidal_Net.Data.Services;

public class TidalTrackServices(TidalRequester requester, string market = "US")
{
    private TidalRequester _requester = requester;

    public async Task<TidalResult<TidalTrack>> GetOne(string trackId)
    {
        try
        {
            var result = await _requester.Request($"tracks/{trackId}?countryCode={market.ToUpper()}");

            if (!result.Success || result.Data is not string json)
                return new(result.Message);

            var tracks = TidalTrack.CreateOne(json);

            return new("Success", tracks, true);

        }
        catch(Exception ex)
        {
            return new(ex.InnerException?.Message ?? ex.Message);

        }
    }
    
    public async Task<TidalResult<List<TidalTrack>>> GetManyTracks(List<string> trackIds)
    {
        try
        {
            var parameters = string.Empty;

            foreach (var id in trackIds)
            {
                if (id == trackIds.Last())
                {
                    parameters += $"filter%5Bid%5D={id}";
                }
                else
                {
                    parameters += $"filter%5Bid%5D={id}&";
                }
            }

            var result = await _requester.Request($"tracks?countryCode={market.ToUpper()}&{parameters}");

            if (!result.Success || result.Data is not string json)
                return new(result.Message);

            var tracks = TidalTrack.CreateMany(json);

            return new("Success", tracks, true);
        }
        catch(Exception ex)
        {
            return new(ex.InnerException?.Message ?? ex.Message);
        }
    }

    
}