using Application.DTOs.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var login = _authService.GetUrl();
            return Ok( new { loginUrl = login });
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery]CallbackRequestDto callbackRequestDto)
        {
            var callback = await _authService.Callback(callbackRequestDto);
            return Ok(callback);
        }

        [HttpGet("token/{id}")]
        public async Task<IActionResult> Token(string id)
        {
            try
            {
                var token = await _tokenService.GetToken(id);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message= "Token not found!" });
            }
            
        }
    }
}
