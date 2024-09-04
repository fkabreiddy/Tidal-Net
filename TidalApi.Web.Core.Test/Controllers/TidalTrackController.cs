using Microsoft.AspNetCore.Mvc;
using TidalApi.Web.Core.Data.Interfaces;

namespace TidalApi.Web.Core.Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TidalTrackController : ControllerBase
{
    private readonly ITidalTrackServices _trackService;

    public TidalTrackController(ITidalTrackServices trackService)
    {
        _trackService = trackService;
    }

    [HttpGet("{trackId}/{token}/{market}")]
    [HttpGet("{trackId}/{token}")]

    public async Task<IActionResult> GetTrack(string trackId, string token, string market = "US")
    {
        var track = await _trackService.GetOne(trackId, token, market);
        if (string.IsNullOrEmpty(track.Id))
            return NotFound();

        return Ok(track);
    }

    [HttpPost("many/{token}")]
    [HttpPost("many/{token}/{market}")]

    public async Task<IActionResult> GetManyTracks([FromBody] List<string> trackIds, string token, string market = "US")
    {
        var tracks = await _trackService.GetManyTracks(trackIds, token, market);
        return Ok(tracks);
    }
}