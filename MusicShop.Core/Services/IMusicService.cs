using MusicShop.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Core.Services
{
    public interface IMusicService
    {
        Task<IEnumerable<Music>> GetAllWithArtists();
        Task<Music> GetMusicById(int id);
        Task<IEnumerable<Music>> GetMusicsByArtistId(int artistId);
        Task<Music> CreateMusic(Music music);
        Task UpdateMusic(Music updateMusic, Music music);
        Task DeleteMusic(Music music);
    }
}
