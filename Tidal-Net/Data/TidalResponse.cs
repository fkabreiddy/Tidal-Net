using Tidal_Net.Authentication;
using Tidal_Net.Data.Services;

namespace Tidal_Net.Data;

public class TidalResponse(string message, string json, bool success = default)
{
    public string Message { get; set; } = message;
    public bool Success { get; set; } = success;

    public string? Json { get; set; } = json;

    public bool IsSuccessed()
    {
        return !string.IsNullOrEmpty(Json) && Success;
    }
}


