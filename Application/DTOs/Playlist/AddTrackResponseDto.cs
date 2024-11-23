using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Playlist
{
    public class AddTrackResponseDto
    {
        [JsonProperty("snapshot_id")]
        public string SnapshotId { get; set; }
    }
}
