using System.Text.Json.Serialization;

namespace Tidal_Net.Data.Models;

public class TidalArtist
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<string> ImageLinks { get; set; } = new();
}