using Application.DTOs.RecentlyPlayed;
using Application.DTOs.TopItems;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/me")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromHeader] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var user = await _userService.GetMe(token);
            return Ok(user);
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTop([FromHeader] string token, [FromQuery] TopItemRequestDto topItemRequest)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            if (topItemRequest.Type == null)
            {
                return BadRequest("Type is required.");
            }

            object res; 
            try
            {
                switch (topItemRequest.Type)
                {
                    case TopItemType.tracks:
                        res = await _userService.GetTopItems<Track>(token, topItemRequest);
                        break;

                    case TopItemType.artists:
                        res = await _userService.GetTopItems<Artist>(token, topItemRequest);
                        break;

                    default:
                        return BadRequest("Invalid type!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok(res);
        }

        [HttpGet("recently-played")]
        public async Task<IActionResult> GetRecentlyPlayed([FromHeader] string token, [FromQuery] RecentlyPlayedRequestDto recentlyPlayedRequest)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var res = await _userService.GetRecentlyPlayed(token, recentlyPlayedRequest);
            return Ok(res);
        }

    }
}
