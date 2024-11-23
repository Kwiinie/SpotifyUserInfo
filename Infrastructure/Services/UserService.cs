using Application.DTOs.Auth;
using Application.DTOs.RecentlyPlayed;
using Application.DTOs.TopItems;
using Application.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<User> GetMe(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is required.", nameof(token));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = "https://api.spotify.com/v1/me";

            var response = await _httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<User>(responseContent);
        }

        public async Task<RecentlyPlayedResponseDto> GetRecentlyPlayed(string token, RecentlyPlayedRequestDto recentlyPlayedRequest)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is required.", nameof(token));
            }

            var limit = recentlyPlayedRequest?.Limit ?? 20;
            


            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = "https://api.spotify.com/v1/me/player/recently-played";

            var queryParams = new List<string>();

            if (limit >= 0 && limit <= 50)
            {
                queryParams.Add($"limit={limit}");
            }
            if (recentlyPlayedRequest != null)
            {
                if (recentlyPlayedRequest.After.HasValue)
                {
                    var after = recentlyPlayedRequest.After.Value.ToUnixTimeMilliseconds();
                    queryParams.Add($"after={after}");
                }

                if (recentlyPlayedRequest.Before.HasValue)
                {
                    if (recentlyPlayedRequest.After.HasValue)
                    {
                        throw new ArgumentException("Cannot specify both 'after' and 'before' parameters.");
                    }
                    var before = recentlyPlayedRequest.Before.Value.ToUnixTimeMilliseconds();
                    queryParams.Add($"before={before}");
                }
            }
            

            var requestUrl = $"{url}?{string.Join("&", queryParams)}";

            var response = await _httpClient.GetAsync(requestUrl);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<RecentlyPlayedResponseDto>(responseContent);
        }

        public async Task<TopItemResponseDto<T>> GetTopItems<T>(string token, TopItemRequestDto topItemRequest)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is required.", nameof(token));
            }

            var type = topItemRequest.Type.ToString();
            var timeRange = topItemRequest.TimeRange?.ToString()?.ToLower() ?? "medium_term";
            var limit = topItemRequest.Limit ?? 20;
            var queryParams = new List<string>
            {
                $"time_range={timeRange}",
                $"limit={limit}"
            };

            if (limit < 0 || limit > 50)
            {
                throw new ArgumentException("Limit must be between 0 and 50.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"https://api.spotify.com/v1/me/top/{type}";

            var requestUrl = $"{url}?{string.Join("&", queryParams)}";

            var response = await _httpClient.GetAsync(requestUrl);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<TopItemResponseDto<T>>(responseContent);

        }

    }
}
