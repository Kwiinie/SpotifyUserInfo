using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RecentlyPlayed
{
    public class ItemDto
    {
        public Track Track { get; set; }
        public DateTime PlayedAt { get; set; }
        public Context Context { get; set; }
    }
}
