using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Recommendation
{
    public class Seed
    {
        [JsonProperty("afterFilteringSize")]
        public int AfterFilteringSize { get; set; }

        [JsonProperty("afterRelinkingSize")]
        public int AfterRelinkingSize { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("initialPoolSize")]
        public int InitialPoolSize { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

    }
}
