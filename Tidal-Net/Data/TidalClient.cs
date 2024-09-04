
using Tidal_Net.Data.Interfaces;

namespace Tidal_Net.Data;

public class TidalClient(
    ITidalAlbumServices albumServices,
    ITidalTrackServices trackServices,
    ITidalArtistServices artistServices) : ITidalClient
{
    
    
    public ITidalAlbumServices Album { get; } = albumServices;
    public ITidalTrackServices Tracks { get; } = trackServices;
    public ITidalArtistServices Artist { get; } = artistServices;
}