using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Recommendation
{
    public class RecommendationRequestDto
    {
        public int Limit { get; set; }
        public string Market { get; set; }
        public string SeedArtists { get; set; }
        public string? SeedGenres { get; set; }
        public string SeedTracks { get; set; }
        public float Acousticness { get; set; }
        public float Danceability { get; set; }
        public int DurationMs { get; set; }
        public float Energy { get; set; }
        public float Instrumentalness { get; set; }
        public int Key {  get; set; }
        public float Liveness { get; set; }
        public float Loudness { get; set; }
        public float Mode { get; set; }
        public float Speechiness { get; set; }
        public float Tempo { get; set; }
        public int TimeSignature { get; set; }
        public float Valence { get; set; }
    }
}
