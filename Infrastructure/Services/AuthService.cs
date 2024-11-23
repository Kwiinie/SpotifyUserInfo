using Application.DTOs.Auth;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthService(IConfiguration configuration, HttpClient httpClient, IUserService userService, ITokenService tokenService)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _userService = userService;
            _tokenService = tokenService;
        }

        public string GetUrl()
        {
            var Url = "https://accounts.spotify.com/authorize";
            var ClientId = _configuration["Spotify:ClientId"];
            var RedirectUri = _configuration["Spotify:RedirectUrl"];
            var ResponseType = "code";
            var Scope = "user-read-private " +
                        "user-read-email " +
                        "user-top-read " +
                        "user-read-recently-played " +
                        "playlist-read-private " +
                        "playlist-modify-public " +
                        "playlist-modify-private " +
                        "user-library-read " +
                        "user-library-modify ";
            var State = Helper.GenerateRandomString(16);
            bool ShowDialog = true;

            return $"{Url}?client_id={ClientId}" +
                   $"&response_type={ResponseType}" +
                   $"&redirect_uri={RedirectUri}" +
                   $"&scope={Scope}" +
                   $"&state={State}" +
                   $"&show_dialog={ShowDialog}";
        }

        public async Task<AuthResponseDto> Callback(CallbackRequestDto callbackRequestDto)
        {
            if (string.IsNullOrEmpty(callbackRequestDto.State))
            {
                callbackRequestDto.Error = "state_mismatch";
                throw new Exception();
            }
            else
            {
                var clientId = _configuration["Spotify:ClientId"];
                var clientSecret = _configuration["Spotify:ClientSecret"];
                var redirectUri = _configuration["Spotify:RedirectUrl"];
                callbackRequestDto.GrantType = "authorization_code";
                var url = "https://accounts.spotify.com/api/token";


                var queryParams = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", callbackRequestDto.GrantType),
                    new KeyValuePair<string, string>("code", callbackRequestDto.Code),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                });

                var response = await _httpClient.PostAsync(url, queryParams);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseContent);
                }
                var result = JsonConvert.DeserializeObject<AuthResponseDto>(responseContent);


                var userId = _userService.GetMe(result.AccessToken).Result.Id;
                await _tokenService.SaveToken(userId, result);

                return result;
            }
        }



    }
}
