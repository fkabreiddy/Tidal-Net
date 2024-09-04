using TidalApi.Web.Core.Data.Models;

namespace TidalApi.Web.Core.Data.Interfaces;

public interface ITidalTrackServices
{
    Task<TidalTrack> GetOne(string trackId, string token, string market = "US");
    Task<List<TidalTrack>> GetManyTracks(List<string> trackIds, string token, string market = "US");
}