using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAudioFeaturesService
    {
        Task<AudioFeatures> GetAudioFeatures(string token, string id);
    }
}
