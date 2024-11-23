using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.ArtistDto
{
    public class ArtistRecommendationResponseDto
    {
        [JsonProperty("artists")]
        public List<Artist> Artists { get; set; }
    }
}
