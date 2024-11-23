using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace WebAPI.Controllers
{
    [Route("api/artist")]
    [ApiController]
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet("recommendation")]
        public async Task<IActionResult> GetRecommendation([FromHeader] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var res = await _artistService.GetArtistRecommendation(token);
            return Ok(res);
        }

        [HttpGet("{id}/top-tracks")]
        public async Task<IActionResult> GetTopTracks([FromHeader] string token, string id)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is required.");
            }

            var res = await _artistService.GetTopTrack(token, id);
            return Ok(res);
        }
    }
}
