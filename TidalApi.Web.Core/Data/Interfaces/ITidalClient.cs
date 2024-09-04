namespace TidalApi.Web.Core.Data.Interfaces;

public interface ITidalClient
{
    ITidalAlbumServices Album { get; }
    ITidalTrackServices Tracks { get; }
    ITidalArtistServices Artist { get; }
}