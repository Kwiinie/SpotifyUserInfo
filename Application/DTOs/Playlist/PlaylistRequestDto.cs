using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Playlist
{
    public class PlaylistRequestDto
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }
}
