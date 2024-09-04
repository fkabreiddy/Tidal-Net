using Tidal_Net.Data.Models;

namespace Tidal_Net.Data.Interfaces;

public interface ITidalAlbumServices
{
    
    /// <summary>
    /// Retrieves a list of Tidal albums based on the provided album IDs.
    /// </summary>
    /// <param name="albumsIds">A list of album IDs to retrieve from the Tidal API.</param>
    /// <param name="token">The authentication token used to access the Tidal API.</param>
    /// <param name="market">The market to be used for the request, with a default value of "us".</param>
    /// <returns>A list of <see cref="TidalAlbum"/> objects corresponding to the provided album IDs. Returns an empty list if the request is unsuccessful.</returns>
    /// <remarks>
    /// This method sends a request to the Tidal API to retrieve information for multiple albums.
    /// If the request is not successful, an empty list is returned.
    /// </remarks>
    Task<List<TidalAlbum>> GetMany(List<string> albumsIds, string token, string market = "us");
    
    /// <summary>
    /// Retrieves a Tidal album based on the provided album ID.
    /// </summary>
    /// <param name="albumId">An album ID to retrieve from the Tidal API.</param>
    /// <param name="token">The authentication token used to access the Tidal API.</param>
    /// <param name="market">The market to be used for the request, with a default value of "us".</param>
    /// <returns>A <see cref="TidalAlbum"/> objects corresponding to the provided album ID. Returns an empty <see cref="TidalAlbum"/> if the request is unsuccessful.</returns>
    /// <remarks>
    /// This method sends a request to the Tidal API to retrieve information for one album.
    /// If the request is not successful, an empty <see cref="TidalAlbum"/> is returned.
    /// </remarks>
    Task<TidalAlbum> GetOne(string albumId, string token, string market = "us");
    
    /// <summary>
    /// Retrieves a list of tracks from a specified Tidal album.
    /// </summary>
    /// <param name="albumId">The ID of the album from which to retrieve tracks.</param>
    /// <param name="token">The authentication token used to access the Tidal API.</param>
    /// <param name="market">The market to be used for the request, with a default value of "us".</param>
    /// <returns>A list of <see cref="TidalTrack"/> objects representing the tracks in the specified album. Returns an empty list if the request is unsuccessful.</returns>
    /// <remarks>
    /// This method sends a request to the Tidal API to retrieve the tracks from a specific album.
    /// If the request is not successful, an empty list is returned.
    /// </remarks>
    Task<List<TidalTrack>> GetTracks(string albumId, string token, string market = "us");
}