using MusicShop.Core;
using MusicShop.Core.Models;
using MusicShop.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Services.Services
{
    public class MusicService : IMusicService
    {
        private readonly IUnitOfWork UnitOfWork;

        public MusicService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<Music> CreateMusic(Music music)
        {
            await UnitOfWork.Musics.AddAsync(music);
            await UnitOfWork.CommitAsync();

            return music;
        }

        public async Task DeleteMusic(Music music)
        {
            UnitOfWork.Musics.Remove(music);
            await UnitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Music>> GetAllWithArtists()
        {
            return await UnitOfWork.Musics.GetAllMusicsWithArtistAsync();
        }

        public async Task<Music> GetMusicById(int id)
        {
            return await UnitOfWork.Musics.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Music>> GetMusicsByArtistId(int artistId)
        {
            return await UnitOfWork.Musics.GetAllMusicsWithArtistByArtistIdAsync(artistId);
        }

        public async Task UpdateMusic(Music updateMusic, Music music)
        {
            updateMusic.Name = music.Name;
            updateMusic.ArtistId = music.ArtistId;

            await UnitOfWork.CommitAsync();
        }
    }
}
