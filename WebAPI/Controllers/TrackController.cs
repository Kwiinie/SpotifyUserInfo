using Application.DTOs.RecentlyPlayed;
using Application.DTOs.SavedTrack;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/track")]
    [ApiController]
    public class TrackController : Controller
    {
        private readonly ITrackService _trackService;

        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet("recommendation")]
        public async Task<IActionResult> GetRecommendation([FromHeader] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var res = await _trackService.GetRecommendation(token);
            return Ok(res);
        }

        [HttpGet("saved")]
        public async Task<IActionResult> GetSavedTrack([FromHeader] string token, [FromQuery] SavedTracksRequestDto request)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var res = await _trackService.GetSavedTracks(token, request);
            return Ok(res);
        }


        [HttpPut("saved/{id}")]
        public async Task<IActionResult> SaveTrack([FromHeader] string token, string id)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Token is required.");
            }

            var res = await _trackService.SaveTrack(token, id);
            return Ok(res);
        }


    }
}
