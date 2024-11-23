using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AudioFeatures
    {
        [JsonProperty("acousticness")]
        public float Acousticness {  get; set; }

        [JsonProperty("analysis_url")]
        public string AnalysisUrl { get; set; }

        [JsonProperty("danceability")]
        public float Danceability { get; set; }

        [JsonProperty("duration_ms")]
        public int DurationMs { get; set; }

        [JsonProperty("energy")]
        public float Energy {  get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("instrumentalness")]
        public float Instrumentalness { get; set; }

        [JsonProperty("key")]
        public int Key { get; set; }

        [JsonProperty("liveness")]
        public float Liveness { get; set; }

        [JsonProperty("loudness")]
        public float Loudness { get; set; }

        [JsonProperty("mode")]
        public int Mode { get; set; }

        [JsonProperty("speechiness")]
        public float Speechiness { get; set; }

        [JsonProperty("tempo")]
        public float Tempo { get; set; }

        [JsonProperty("time_signature")]
        public int TimeSignature { get; set; }

        [JsonProperty("track_href")]
        public string TrackHref { get; set; }

        [JsonProperty("type")]
        public string type { get; set; } = "audio_features";

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("valence")]
        public float Valence { get; set; }

    }
}
