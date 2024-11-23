using Application.DTOs.Playlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPlaylistService
    {
        Task<PlaylistResponseDto> GetMyPlaylist(string token, PlaylistRequestDto request);
        Task<PlaylistTrackResponseDto> GetPlaylistTracks(string token, string id);
        Task<AddTrackResponseDto> AddTrackToPlaylist (string token, string id, AddTrackRequestDto request);

    }
}
