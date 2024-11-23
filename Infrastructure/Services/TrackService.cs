using Application.DTOs.Playlist;
using Application.DTOs.Recommendation;
using Application.DTOs.SavedTrack;
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
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TrackService : ITrackService
    {
        private readonly IAudioFeaturesService _audioFeaturesService;
        private readonly HttpClient _httpClient;
        private readonly IUserService _userService;

        public TrackService(IAudioFeaturesService audioFeaturesService, HttpClient httpClient, IUserService userService)
        {
            _audioFeaturesService = audioFeaturesService;
            _httpClient = httpClient;
            _userService = userService;
        }


        public async Task<RecommendationResponseDto> GetRecommendation(string token)
        {
            var request = new RecommendationRequestDto();
            var topTrackRequest = new TopItemRequestDto()
            {
                Type = TopItemType.tracks,
                Limit = 20
            };
            var topArtistRequest = new TopItemRequestDto()
            {
                Type = TopItemType.artists,
                Limit = 20
            };
            var topTrackResponse = await _userService.GetTopItems<Track>(token, topTrackRequest);
            var topArtistResponse = await _userService.GetTopItems<Artist>(token, topArtistRequest);
            var recentlyPlayed = await _userService.GetRecentlyPlayed(token, null);
            var recentTracks = recentlyPlayed.Items.Select(item => item.Track).ToList();

            List<AudioFeatures> audioFeatures = new List<AudioFeatures>();
            var randomTracks = Helper.GetRandomObjects<Track>(topTrackResponse.Items, 7);
            foreach (var track in randomTracks)
            {
                var audio = await _audioFeaturesService.GetAudioFeatures(token, track.Id);
                audioFeatures.Add(audio);
            }

            
            var floatProperties = typeof(AudioFeatures).GetProperties()
                .Where(p => p.PropertyType == typeof(float))
                .ToList();

            var intProperties = typeof(AudioFeatures).GetProperties()
                .Where(p => p.PropertyType == typeof(int))
                .ToList();

            foreach (var property in floatProperties)
            {
                var values = audioFeatures.Select(a => (float)property.GetValue(a)).ToList();
                float average = Helper.CalculateAverageFloat(values);

                var targetProperty = typeof(RecommendationRequestDto).GetProperty($"{property.Name}");
                if (targetProperty != null && targetProperty.PropertyType == typeof(float))
                {
                    targetProperty.SetValue(request, average);
                }
            }

            foreach (var property in intProperties)
            {
                var values = audioFeatures.Select(a => (int)property.GetValue(a)).ToList();
                int average = Helper.CalculateAverageInt(values);

                var targetProperty = typeof(RecommendationRequestDto).GetProperty($"{property.Name}");
                if (targetProperty != null && targetProperty.PropertyType == typeof(int))
                {
                    targetProperty.SetValue(request, average);
                }
            }

            var seedTopTrack = Helper.GetRandomObjects<Track>(topTrackResponse.Items, 2);
            var seedRecentTrack = Helper.GetRandomObjects<Track>(recentTracks, 1);
            var seedArtist = Helper.GetRandomObjects<Artist>(topArtistResponse.Items, 2);

            var seedTopTrackIds = seedTopTrack.Select(track => track.Id).ToList();
            var seedRecentTrackIds = seedRecentTrack.Select(track => track.Id).ToList();
            var seedArtistIds = seedArtist.Select(artist => artist.Id).ToList();

            var allTrackIds = seedTopTrackIds.Concat(seedRecentTrackIds).ToList();
            var seedTrackIdString = string.Join(",", allTrackIds);
            var seedArtistIdString = string.Join(",", seedArtistIds);

            request.SeedTracks = seedTrackIdString;
            request.SeedArtists = seedArtistIdString;


            var queryParams = new List<string>
            {
                $"limit=20",
                $"seed_artists={request.SeedArtists}",
                $"seed_tracks={request.SeedTracks}",
                $"target_acousticness={request.Acousticness.ToString(CultureInfo.InvariantCulture)}",
                $"target_danceability={request.Danceability.ToString(CultureInfo.InvariantCulture)}",
                $"target_duration_ms={request.DurationMs.ToString(CultureInfo.InvariantCulture)}",
                $"target_energy={request.Energy.ToString(CultureInfo.InvariantCulture)}",
                $"target_instrumentalness={request.Instrumentalness.ToString(CultureInfo.InvariantCulture)}",
                $"target_key={request.Key}",
                $"target_liveness={request.Liveness.ToString(CultureInfo.InvariantCulture)}",
                $"target_loudness={request.Loudness.ToString(CultureInfo.InvariantCulture)}",
                $"target_mode={request.Mode.ToString(CultureInfo.InvariantCulture)}",
                $"target_speechiness={request.Speechiness.ToString(CultureInfo.InvariantCulture)}",
                $"target_time_signature={request.TimeSignature}",
                $"target_valence={request.Valence.ToString(CultureInfo.InvariantCulture)}"

            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = "https://api.spotify.com/v1/recommendations";

            var requestUrl = $"{url}?{string.Join("&", queryParams)}";

            var response = await _httpClient.GetAsync(requestUrl);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<RecommendationResponseDto>(responseContent);

        }

        public async Task<bool> CheckSavedTrack(string token, string id)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"https://api.spotify.com/v1/me/tracks/contains?ids={id}";
            var response = await _httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<bool[]>(responseContent)[0];
        }


        public async Task<SavedTracksResponseDto> GetSavedTracks(string token, SavedTracksRequestDto request)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is required.", nameof(token));
            }

            var limit = request?.Limit ?? 20;
            var offset = request?.Offset ?? 0;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = "https://api.spotify.com/v1/me/tracks";

            var queryParams = new List<string>();

            if (limit >= 0 && limit <= 50)
            {
                queryParams.Add($"limit={limit}");
            }
            queryParams.Add($"offset={offset}");

            var requestUrl = $"{url}?{string.Join("&", queryParams)}";

            var response = await _httpClient.GetAsync(requestUrl);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return JsonConvert.DeserializeObject<SavedTracksResponseDto>(responseContent);
        }

        public async Task<string> SaveTrack(string token, string id)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is required.", nameof(token));
            }
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Playlist ID is required.", nameof(id));
            }
            bool isSaved = await CheckSavedTrack(token, id);
            if(isSaved == true)
            {
                return "Track was already saved!";
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"https://api.spotify.com/v1/me/tracks?ids={id}";
            var response = await _httpClient.PutAsync(url, null);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return "Saving track successfully";
        }
    }
}
