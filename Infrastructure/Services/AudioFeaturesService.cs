using Application.DTOs.RecentlyPlayed;
using Application.Interfaces;
using Domain.Entities;
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
    public class AudioFeaturesService : IAudioFeaturesService
    {
        private readonly HttpClient _httpClient;

        public AudioFeaturesService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<AudioFeatures> GetAudioFeatures(string token, string id)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is required.", nameof(token));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"https://api.spotify.com/v1/audio-features/{id}";

            var response = await _httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<AudioFeatures>(responseContent);
        }
    }
}
