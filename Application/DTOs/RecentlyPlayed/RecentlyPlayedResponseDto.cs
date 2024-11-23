using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.DTOs.RecentlyPlayed
{
    public class RecentlyPlayedResponseDto
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("cursors")]
        public Cursor Cursor { get; set; }

        [JsonProperty("total")]
        public int Total {  get; set; }

        [JsonProperty("items")]
        public List<ItemDto> Items { get; set; }
    }
}
