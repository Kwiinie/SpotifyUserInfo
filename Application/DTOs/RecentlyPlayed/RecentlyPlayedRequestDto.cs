using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RecentlyPlayed
{
    public class RecentlyPlayedRequestDto
    {
        public int? Limit {  get; set; }
        public DateTimeOffset? After { get; set; }
        public DateTimeOffset? Before { get; set; }
    }
}
