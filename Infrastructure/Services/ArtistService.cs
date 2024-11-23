using Application.DTOs.Recommendation;
using Application.DTOs.TopItems;
using Application.Helpers;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.ArtistDto;
using Application.DTOs.Playlist;

namespace Infrastructure.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;

        public ArtistService(IUserService userService, HttpClient httpClient)
        {
            _userService = userService;
            _httpClient = httpClient;

        }

        public async Task<ArtistRecommendationResponseDto> GetArtistRecommendation(string token)
        {
            var topArtistRequest = new TopItemRequestDto()
            {
                Type = TopItemType.artists,
                Limit = 20
            };
            var topArtistResponse = await _userService.GetTopItems<Artist>(token, topArtistRequest);
            var randomArtist = Helper.GetRandomObjects<Artist>(topArtistResponse.Items, 1);
            var queryParams = randomArtist[0].Id;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"https://api.spotify.com/v1/artists/{queryParams}/related-artists";
            var response = await _httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<ArtistRecommendationResponseDto>(responseContent);
        }

        public async Task<ArtistTopTrackResponseDto> GetTopTrack(string token, string id)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"https://api.spotify.com/v1/artists/{id}/top-tracks";
            var response = await _httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<ArtistTopTrackResponseDto>(responseContent);
        }
    }
}
