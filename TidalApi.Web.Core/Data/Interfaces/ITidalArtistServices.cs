using TidalApi.Web.Core.Data.Models;

namespace TidalApi.Web.Core.Data.Interfaces;

public interface ITidalArtistServices
{
    /// <summary>
    /// Retrieves information about a specific Tidal artist.
    /// </summary>
    /// <param name="artistId">The ID of the artist to retrieve information for.</param>
    /// <param name="token">The authentication token used to access the Tidal API.</param>
    /// <param name="market">The market to be used for the request, with a default value of "US".</param>
    /// <returns>A <see cref="TidalArtist"/> object containing information about the specified artist. Returns a new, empty instance if the request is unsuccessful.</returns>
    /// <remarks>
    /// This method sends a request to the Tidal API to retrieve information about a specific artist.
    /// If the request is not successful, an empty artist object is returned.
    /// </remarks>
    Task<TidalArtist> GetOne(string artistId, string token, string market = "US");
    
    /// <summary>
    /// Retrieves information about multiple Tidal artists based on the provided artist IDs.
    /// </summary>
    /// <param name="artistIds">A list of artist IDs to retrieve information for.</param>
    /// <param name="token">The authentication token used to access the Tidal API.</param>
    /// <param name="market">The market to be used for the request, with a default value of "US".</param>
    /// <returns>A list of <see cref="TidalArtist"/> objects corresponding to the provided artist IDs. Returns an empty list if the request is unsuccessful.</returns>
    /// <remarks>
    /// This method sends a request to the Tidal API to retrieve information for multiple artists.
    /// If the request is not successful, an empty list is returned.
    /// </remarks>
    Task<List<TidalArtist>> GetMany(List<string> artistIds, string token, string market = "US");
}