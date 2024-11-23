using Application.DTOs.Auth;
using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        Task SaveToken(string userId, AuthResponseDto authResponseDto);
        Task<Token> GetToken (string userId);
    }
}
