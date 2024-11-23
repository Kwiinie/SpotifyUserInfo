using Application.DTOs.Playlist;
using Application.DTOs.RecentlyPlayed;
using Application.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly HttpClient _httpClient;

        public PlaylistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<PlaylistResponseDto> GetMyPlaylist(string token, PlaylistRequestDto request)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is required.", nameof(token));
            }

            var limit = request?.Limit ?? 20;
            var offset = request?.Offset ?? 0;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = "https://api.spotify.com/v1/me/playlists";

            var queryParams = new List<string>();

            if (limit >= 0 && limit <= 50)
            {
                queryParams.Add($"limit={limit}");
            }
            if (offset >= 0 && offset <= 100000)
            {
                queryParams.Add($"offset={offset}");
            }

            var requestUrl = $"{url}?{string.Join("&", queryParams)}";

            var response = await _httpClient.GetAsync(requestUrl);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<PlaylistResponseDto>(responseContent);
        }

        public async Task<PlaylistTrackResponseDto> GetPlaylistTracks(string token, string id)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is required.", nameof(token));
            }
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Playlist ID is required.", nameof(id));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"https://api.spotify.com/v1/playlists/{id}/tracks";
            var response = await _httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<PlaylistTrackResponseDto>(responseContent);
        }

        public async Task<AddTrackResponseDto> AddTrackToPlaylist(string token, string id, AddTrackRequestDto request)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is required.", nameof(token));
            }
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Playlist ID is required.", nameof(id));
            }
            if (request == null || request.Uris == null || !request.Uris.Any())
            {
                throw new ArgumentException("At least one track URI is required.", nameof(request));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string queryParams = string.Join(",", request.Uris);
            var url = $"https://api.spotify.com/v1/playlists/{id}/tracks?uris={queryParams}";
            var response = await _httpClient.PostAsync(url, null);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<AddTrackResponseDto>(responseContent);
        }

    }
}
