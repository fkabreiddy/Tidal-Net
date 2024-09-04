using Microsoft.AspNetCore.Mvc;
using TidalApi.Web.Core.Data.Interfaces;

namespace TidalApi.Web.Core.Test.Controllers;


    [ApiController]
    [Route("api/[controller]")]
    public class TidalAlbumController : Controller
    {
        private readonly ITidalAlbumServices _albumService;

        public TidalAlbumController(ITidalAlbumServices albumService)
        {
            _albumService = albumService;
        }

        [HttpGet("{albumId}/{token}/{market?}")]
        [HttpGet("{albumId}/{token}")]
        public async Task<IActionResult> GetAlbum(string albumId, string token, string market = "US")
        {
            var album = await _albumService.GetOne(albumId, token, market);
            if (string.IsNullOrEmpty(album.Id))
                return NotFound();

            return Ok(album);
        }

        [HttpPost("many/{token}/{market?}")]
        [HttpPost("many/{token}")]
        public async Task<IActionResult> GetManyAlbums([FromBody] List<string> albumIds, string token, string market = "US")
        {
            var albums = await _albumService.GetMany(albumIds, token, market);
            return Ok(albums);
        }
        
        [HttpGet("{albumId}/tracks/{token}/{market?}")]
        [HttpGet("{albumId}/tracks/{token}")]

        public async Task<IActionResult> GetAlbumTracks(string albumId, string token, string market = "US")
        {
            var tracks = await _albumService.GetTracks(albumId, token, market);
            if (!tracks.Any())
                return NotFound();

            return Ok(tracks);
        }
    }