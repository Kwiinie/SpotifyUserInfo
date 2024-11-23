using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TopItems
{
    public class TopItemRequestDto
    {
        public TopItemType Type { get; set; }
        public TopItemTimeRange? TimeRange { get; set; }
        public int? Limit { get; set; }
    }
}
