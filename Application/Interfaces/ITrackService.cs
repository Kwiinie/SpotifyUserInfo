using Application.DTOs.Recommendation;
using Application.DTOs.SavedTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITrackService
    {
        Task<RecommendationResponseDto> GetRecommendation(string token);
        Task<SavedTracksResponseDto> GetSavedTracks(string token, SavedTracksRequestDto request);
        Task<string> SaveTrack(string token, string id);
        Task<bool> CheckSavedTrack(string token, string id);
    }
}
