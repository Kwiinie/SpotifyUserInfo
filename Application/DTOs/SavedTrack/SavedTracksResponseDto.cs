﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SavedTrack
{
    public class SavedTracksResponseDto
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("items")]
        public List<ItemDto> Items { get; set; }
    }
}
