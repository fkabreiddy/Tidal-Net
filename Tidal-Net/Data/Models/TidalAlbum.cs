

using Newtonsoft.Json;

namespace Tidal_Net.Data.Models
{
    public class TidalAlbum
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int NumberOfItems { get; set; }
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
                var response = JsonConvert.DeserializeObject<dynamic>(json);

                if (response is null)
                    return albumsToReturn;

                if (response.data == null)
                    return albumsToReturn;

                foreach (var albumData in response.data)
                {
                    var newAlbum = new TidalAlbum
                    {
                        Title = albumData.attributes?.title ?? "",
                        BarcodeId = albumData.attributes?.barcodeId ?? "",
                        NumberOfItems = albumData.attributes?.numberOfItems ?? 0,
                        ReleaseDate = albumData.attributes?.releaseDate ?? DateTime.Now,
                        Copyright = albumData.attributes?.copyright ?? "",
                        Id = albumData.id ?? ""
                    };

                    // Mapear enlaces de imagen
                    if (albumData.attributes?.imageLinks != null)
                    {
                        foreach (var imageLink in albumData.attributes.imageLinks)
                        {
                            newAlbum.ImageLinks.Add((string)imageLink.href);
                        }
                    }

                    // Mapear artistas
                    if (response.included != null)
                    {
                        foreach (var artist in response.included)
                        {
                            if (artist.type == "artist")
                            {
                                var artistObj = new TidalArtist
                                {
                                    Id = artist.id ?? "",
                                    Name = artist.attributes?.name ?? ""
                                };

                                // Mapear enlaces de imagen de artistas
                                if (artist.attributes?.imageLinks != null)
                                {
                                    foreach (var artistImage in artist.attributes.imageLinks)
                                    {
                                        artistObj.ImageLinks.Add((string)artistImage.href);
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
                NumberOfItems = albumData.attributes?.numberOfItems ?? 0,
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
                    if (artist.type == "artist")
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
    }
        
   
    
}
