using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ArtistDto
{
    public class ArtistTopTrackResponseDto
    {
        [JsonProperty("tracks")]
        public List<Track> Tracks { get; set; }
    }
}
