namespace Tidal_Net.Data.Models
{
    public class TidalArtist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Picture> Picture { get; set; }
        public bool Main { get; set; }
    }
}
