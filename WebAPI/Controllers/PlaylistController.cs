using Application.DTOs.Playlist;
using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/playlist")]
    [ApiController]
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService _playlistService;
        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Get([FromHeader] string token, [FromQuery] PlaylistRequestDto request)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var playlist = await _playlistService.GetMyPlaylist(token, request);
            return Ok(playlist);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTracks([FromHeader] string token, string id)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var playlist = await _playlistService.GetPlaylistTracks(token, id);
            return Ok(playlist);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddTracks([FromHeader] string token, string id, [FromBody] AddTrackRequestDto request)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var playlist = await _playlistService.AddTrackToPlaylist(token, id, request);
            return Ok(playlist);
        }
    }
}
