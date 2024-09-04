using Microsoft.AspNetCore.Mvc;
using TidalApi.Web.Core.Data.Interfaces;

namespace TidalApi.Web.Core.Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TidalArtistController : Controller
{
    private readonly ITidalArtistServices _artistService;

    public TidalArtistController(ITidalArtistServices artistService)
    {
        _artistService = artistService;
    }

    [HttpGet("{artistId}/{token}/{market}")]
    [HttpGet("{artistId}/{token}")]
    public async Task<IActionResult> GetArtist(string artistId, string token, string market = "US")
    {
        var artist = await _artistService.GetOne(artistId, token, market);
        if (string.IsNullOrEmpty(artist.Id))
            return NotFound();

        return Ok(artist);
    }

    [HttpPost("many/{token}/{market}")]
    [HttpPost("many/{token}")]

    public async Task<IActionResult> GetManyArtists([FromBody] List<string> artistIds, string token, string market = "US")
    {
        var artists = await _artistService.GetMany(artistIds, token, market);
        return Ok(artists);
    }
}