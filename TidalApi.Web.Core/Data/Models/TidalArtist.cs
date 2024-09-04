using Newtonsoft.Json;

namespace TidalApi.Web.Core.Data.Models;

public class TidalArtist
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<string> ImageLinks { get; set; } = new();
    
    public static TidalArtist CreateOne(string json)
    {
        var response = JsonConvert.DeserializeObject<dynamic>(json);
    
        if (response?.data == null)
            return new TidalArtist();

        var artistData = response.data;

        var artist = new TidalArtist
        {
            Id = artistData.id ?? "",
            Name = artistData.attributes?.name ?? ""
        };

        if (artistData.attributes?.imageLinks != null)
        {
            foreach (var imageLink in artistData.attributes.imageLinks)
            {
                artist.ImageLinks.Add((string)imageLink.href);
            }
        }

        return artist;
    }
    public static List<TidalArtist> CreateMany(string json)
    {
        var artists = new List<TidalArtist>();
    
        var response = JsonConvert.DeserializeObject<dynamic>(json);
    
        if (response?.data == null)
            return artists;

        foreach (var artistData in response.data)
        {
            var artist = new TidalArtist
            {
                Id = artistData.id ?? "",
                Name = artistData.attributes?.name ?? ""
            };

            if (artistData.attributes?.imageLinks != null)
            {
                foreach (var imageLink in artistData.attributes.imageLinks)
                {
                    artist.ImageLinks.Add((string)imageLink.href);
                }
            }

            artists.Add(artist);
        }

        return artists;
    }

}