using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>();
            services.AddHttpClient<IUserService, UserService>();
            services.AddHttpClient<ITrackService, TrackService>();
            services.AddHttpClient<IAudioFeaturesService, AudioFeaturesService>();
            services.AddHttpClient<IPlaylistService, PlaylistService>();
            services.AddHttpClient<IArtistService, ArtistService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
