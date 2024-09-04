
using TidalApi.Web.Core.Data.Interfaces;

namespace TidalApi.Web.Core.Data;

public class TidalClient(
    ITidalAlbumServices albumServices,
    ITidalTrackServices trackServices,
    ITidalArtistServices artistServices) : ITidalClient
{
    
    
    public ITidalAlbumServices Album { get; } = albumServices;
    public ITidalTrackServices Tracks { get; } = trackServices;
    public ITidalArtistServices Artist { get; } = artistServices;
}