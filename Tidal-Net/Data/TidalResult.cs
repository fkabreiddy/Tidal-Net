using Tidal_Net.Authentication;
using Tidal_Net.Data.Services;

namespace Tidal_Net.Data;

public class TidalResult(string message, bool success = default)
{
    public string Message { get; set; } = message;
    public bool Success { get; set; } = success;
}

public class TidalResult<T>(string message,  T? data = default, bool success = default)
{
    public string Message { get; set; } = message;
    public bool Success { get; set; } = success;
    public T? Data { get; set; } = data;

   
    
}
