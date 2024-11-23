using Application.DTOs.ArtistDto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IArtistService
    {
        Task<ArtistRecommendationResponseDto> GetArtistRecommendation(string token);
        Task<ArtistTopTrackResponseDto> GetTopTrack(string token, string id);
    }
}
