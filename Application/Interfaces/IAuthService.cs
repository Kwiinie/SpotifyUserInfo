using Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        string GetUrl();
        Task<AuthResponseDto> Callback(CallbackRequestDto callbackRequestDto);
    }
}
