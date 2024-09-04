using Microsoft.AspNetCore.Mvc;
using TidalApi.Web.Core.Data.Interfaces;

namespace TidalApi.Web.Core.Test.Controllers;

public class TokenController(ITidalTokenBuilder tokenBuilder) : Controller
{
    private readonly ITidalTokenBuilder _tokenBuilder = tokenBuilder;
    [HttpGet("token/{client_id}/{client_secret}")]
    public async Task<IActionResult> GetToken(string client_id, string client_secret)
    {
        try
        {
            var token = await _tokenBuilder.Build(client_id, client_secret);

            return  string.IsNullOrEmpty(token) ? BadRequest("There was a problem building the token") : Ok(token);

        }
        catch(Exception ex)
        {
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }
    
}