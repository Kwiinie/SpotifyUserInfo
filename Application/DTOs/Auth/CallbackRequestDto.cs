using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class CallbackRequestDto
    {
        public string Code { get; set; }
        public string State { get; set; }
        public string? GrantType { get; set; }
        public string? Error { get; set; }
    }
}
