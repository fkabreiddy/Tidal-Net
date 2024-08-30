using Tidal_Net.Data.Models;

namespace Tidal_Net.Data.Services;

public class TidalArtistServices(TidalRequester requester, string market = "US")
{
    private TidalRequester _requester = requester;

    public async Task<TidalResult<TidalArtist>> GetOne(string artistId)
    {
        try
        {
            var result = await _requester.Request($"artists/{artistId}?countryCode={market.ToUpper()}");

            if (!result.Success || result.Data is not string json)
                return new(result.Message);

            var artists = TidalArtist.CreateOne(json);

            return new("Success", artists, true);

        }
        catch(Exception ex)
        {
            return new(ex.InnerException?.Message ?? ex.Message);
        }
    }
    public async Task<TidalResult<List<TidalArtist>>> GetMany(List<string> artistIds)
    {
        try
        {
            
            var parameters = string.Empty;

            foreach (var id in artistIds)
            {
                if (id == artistIds.Last())
                {
                    parameters += $"filter%5Bid%5D={id}";
                }
                else
                {
                    parameters += $"filter%5Bid%5D={id}&";
                }
               
            }
            var result = await _requester.Request($"artists?countryCode={market.ToUpper()}&{parameters}");

            if (!result.Success || result.Data is not string json)
                return new(result.Message);

            var artists = TidalArtist.CreateMany(json);

            return new("Success", artists, true);

        }
        catch(Exception ex)
        {
            return new(ex.InnerException?.Message ?? ex.Message);
        }
    }
}