namespace Tidal_Net.Data.Utilities;

public class TidalEndpoints(string parameters, string market)
{
    private string Parameters { get; } = parameters ?? throw new ArgumentNullException(nameof(parameters));
    private string Market { get; } = market.ToUpper() ?? throw new AggregateException(nameof(market));

    public string ManyAlbums => $"albums?countryCode={Market}&include=artists&{Parameters}";
    public string OneAlbum => $"albums/{parameters}?countryCode={market.ToUpper()}&include=artists";

    public string AlbumTracks => $"albums/{parameters}?countryCode={market.ToUpper()}&include=items&include=artists";

    public string OneArtist => $"artists/{parameters}?countryCode={market.ToUpper()}";

    public string ManyArtists => $"artists?countryCode={market.ToUpper()}&{parameters}";

    public string OneTrack => $"tracks/{parameters}?countryCode={market.ToUpper()}";

    public string ManyTracks => $"tracks?countryCode={market.ToUpper()}&{parameters}";


}