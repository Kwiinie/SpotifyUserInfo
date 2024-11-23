using Domain.Entities;
using Domain.Entities.Recommendation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Recommendation
{
    public class RecommendationResponseDto
    {
        [JsonProperty("seeds")]
        public List<Seed> Seeds { get; set; }
        [JsonProperty("tracks")]
        public List<Track> Tracks { get; set; }
    }
}
