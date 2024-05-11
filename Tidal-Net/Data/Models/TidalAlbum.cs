using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tidal_Net.Data.Models
{
    public class TidalAlbum
    {
        [JsonPropertyName("[resources][id]")]
        public string Id { get; set; }
        public string BarcodeId { get; set; }
        public string Title { get; set; }
        public List<TidalArtist> Artists { get; set; }
        public int Duration { get; set; }
        public string ReleaseDate { get; set; }
        public List<ImageCover> ImageCovers { get; set; }
        public List<ImageCover> VideoCovers { get; set; }
        public int NumberOfVolumes { get; set; }
        public int NumberOfTracks { get; set; }
        public int NumberOfVideos { get; set; }
        public string Type { get; set; }
        public string Copyright { get; set; }
        
        public string TidalUrl { get; set; }
        public double Popularity { get; set; }
    }

}
