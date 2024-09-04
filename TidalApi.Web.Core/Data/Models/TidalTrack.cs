using Newtonsoft.Json.Linq;

namespace TidalApi.Web.Core.Data.Models;

public class TidalTrack
{
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Version { get; set; } = string.Empty;
    
    public bool Explicit { get; set; }
    
    
    

    public static List<TidalTrack> CreateMany(string json)
    {
        var tracks = new List<TidalTrack>();
        
        var jObject = JObject.Parse(json);
        var data = jObject["data"];

        if (data == null)
            return tracks;

        foreach (var trackData in data)
        {
            var attributes = trackData["attributes"];
            
            var track = new TidalTrack
            {
                Id = trackData["id"]?.ToString() ?? string.Empty,
                Title = attributes?["title"]?.ToString() ?? string.Empty,
                Explicit = (bool?)attributes?["explicit"] ?? false
            };

            tracks.Add(track);
        }

        return tracks;
    }
    public static TidalTrack CreateOne(string json)
    {
        
        var jObject = JObject.Parse(json);
        var data = jObject["data"];

        if (data == null)
            return new TidalTrack();

       
        var attributes = data["attributes"];
        
        var track = new TidalTrack
        {
            Id = data["id"]?.ToString() ?? string.Empty,
            Title = attributes?["title"]?.ToString() ?? string.Empty,
            Explicit = (bool?)attributes?["explicit"] ?? false
        };

            
        

        return track;
    }
}