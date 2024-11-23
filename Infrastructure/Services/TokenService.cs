using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Entities.Auth;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConnectionMultiplexer _redis;

        public TokenService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<Token> GetToken(string userId)
        {
            var db = _redis.GetDatabase();

            // Truy xuất dữ liệu từ Redis
            var tokenJson = await db.StringGetAsync(userId);
            if (string.IsNullOrEmpty(tokenJson))
            {
                throw new Exception("Token not found or expired");
            }

            var tokenData = JsonSerializer.Deserialize<Token>(tokenJson);

            return tokenData;
        }

        public async Task SaveToken(string userId, AuthResponseDto authResponseDto)
        {
            var db = _redis.GetDatabase();
            var token = new Token();
            token.AccessToken = authResponseDto.AccessToken;
            token.RefreshToken = authResponseDto.RefreshToken;
            token.ExpiresIn = authResponseDto.ExpiresIn;

            var tokenJson = JsonSerializer.Serialize(token);
            await db.StringSetAsync(userId, tokenJson, TimeSpan.FromSeconds(token.ExpiresIn));

        }
    }
}
