using Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Context
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }

        [JsonProperty("type")]
        public ContextType Type { get; set; }
    }
}
