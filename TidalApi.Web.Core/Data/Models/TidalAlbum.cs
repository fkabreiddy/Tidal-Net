using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TidalApi.Web.Core.Data.Models
{
    public class TidalAlbum
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int NumberOfTracks { get; set; }
        public string BarcodeId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Copyright { get; set; } = string.Empty;
        public List<string> ImageLinks { get; set; } = new();
        public List<TidalArtist> Artists { get; set; } = new();
        

        public static List<TidalAlbum> CreateMany(string json)
        {
            var albumsToReturn = new List<TidalAlbum>();

            try
            {
                var response = JObject.Parse(json);

                var data = response["data"] as JArray;
                if (data == null)
                    return albumsToReturn;

                var included = response["included"] as JArray;

                foreach (var albumData in data)
                {
                    var newAlbum = new TidalAlbum
                    {
                        Title = albumData["attributes"]?["title"]?.ToString() ?? "",
                        BarcodeId = albumData["attributes"]?["barcodeId"]?.ToString() ?? "",
                        NumberOfTracks = albumData["attributes"]?["numberOfItems"]?.ToObject<int>() ?? 0,
                        ReleaseDate = albumData["attributes"]?["releaseDate"]?.ToObject<DateTime>() ?? DateTime.Now,
                        Copyright = albumData["attributes"]?["copyright"]?.ToString() ?? "",
                        Id = albumData["id"]?.ToString() ?? ""
                    };

                    // Mapear enlaces de imagen
                    var imageLinks = albumData["attributes"]?["imageLinks"] as JArray;
                    if (imageLinks != null)
                    {
                        foreach (var imageLink in imageLinks)
                        {
                            newAlbum.ImageLinks.Add(imageLink["href"]?.ToString() ?? "");
                        }
                    }

                    // Mapear artistas
                    if (included != null)
                    {
                        foreach (var artist in included)
                        {
                            var artistsRelationships = albumData["relationships"]?["artists"]?["data"] as JArray;
                            if (artist["type"]?.ToString() == "artists" && artistsRelationships != null &&
                                artistsRelationships.Any(a => a["id"]?.ToString() == artist["id"]?.ToString()))
                            {
                                var artistObj = new TidalArtist
                                {
                                    Id = artist["id"]?.ToString() ?? "",
                                    Name = artist["attributes"]?["name"]?.ToString() ?? ""
                                };

                                // Mapear enlaces de imagen de artistas
                                var artistImageLinks = artist["attributes"]?["imageLinks"] as JArray;
                                if (artistImageLinks != null)
                                {
                                    foreach (var artistImage in artistImageLinks)
                                    {
                                        artistObj.ImageLinks.Add(artistImage["href"]?.ToString() ?? "");
                                    }
                                }

                                newAlbum.Artists.Add(artistObj);
                            }
                        }
                    }

                    albumsToReturn.Add(newAlbum);
                }

                return albumsToReturn;
            }
            catch
            {
                return albumsToReturn;
            }
        }

        public static TidalAlbum Create(string json)
        {
            var response = JsonConvert.DeserializeObject<dynamic>(json);
            if (response is null)
                return new TidalAlbum();

            var albumData = response.data;
            if (albumData == null)
                return new TidalAlbum();

            var album = new TidalAlbum
            {
                Title = albumData.attributes?.title ?? "",
                BarcodeId = albumData.attributes?.barcodeId ?? "",
                NumberOfTracks = albumData.attributes?.numberOfItems ?? 0,
                ReleaseDate = albumData.attributes?.releaseDate ?? DateTime.Now,
                Copyright = albumData.attributes?.copyright ?? "",
                Id = albumData.id ?? ""
            };

            if (albumData.attributes?.imageLinks != null)
            {
                foreach (var imageLink in albumData.attributes.imageLinks)
                {
                    album.ImageLinks.Add((string)imageLink.href);
                }
            }

            if (response.included != null)
            {
                foreach (var artist in response.included)
                {
                    if (artist.type == "artists")
                    {
                        var artistObj = new TidalArtist
                        {
                            Id = artist.id ?? "",
                            Name = artist.attributes?.name ?? ""
                        };

                        if (artist.attributes?.imageLinks != null)
                        {
                            foreach (var artistImage in artist.attributes.imageLinks)
                            {
                                artistObj.ImageLinks.Add((string)artistImage.href);
                            }
                        }

                        album.Artists.Add(artistObj);
                    }
                }
            }

            return album;
        }

        public static List<TidalTrack> GetTracks(string json)
        {
            var response = JObject.Parse(json);



            var included = response["included"];

            if (included is null)
                return new();
            
            var tracks = new List<TidalTrack>();
            
            foreach (var track in included.Where(i => i?["type"]?.ToString() == "tracks"))
            {
                var newtrack = new TidalTrack()
                {
                    Id = track?["id"]?.ToString() ?? string.Empty,
                    Title = track?["attributes"]?["title"]?.ToString() ?? string.Empty,
                    Explicit = (bool?)track?["attributes"]?["explicit"] ?? false
                };
                
                tracks.Add(newtrack);
            }

            return tracks;
        }
    }
        
   
    
}
