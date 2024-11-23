using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Playlist
{
    public class AddTrackRequestDto
    {
        public List<string> Uris { get; set; }
    }
}
