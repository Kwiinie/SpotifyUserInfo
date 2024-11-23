using Application.DTOs.RecentlyPlayed;
using Application.DTOs.TopItems;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetMe(string token);
        Task<TopItemResponseDto<T>> GetTopItems<T>(string token, TopItemRequestDto topItemRequest);
        Task<RecentlyPlayedResponseDto> GetRecentlyPlayed (string token, RecentlyPlayedRequestDto recentlyPlayedRequest);
    }
}
