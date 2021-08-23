using MusicShop.Core;
using MusicShop.Core.Models;
using MusicShop.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Services.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork UnitOfWork;

        public ArtistService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<Artist> CreateArtist(Artist artist)
        {
            await UnitOfWork.Artists.AddAsync(artist);
            await UnitOfWork.CommitAsync();

            return artist;
        }

        public async Task DeleteArtist(Artist artist)
        {
            UnitOfWork.Artists.Remove(artist);
            await UnitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Artist>> GetAllArtists()
        {
            return await UnitOfWork.Artists.GetAllArtistsWithMusicsAsync();
        }

        public async Task<Artist> GetArtistById(int id)
        {
            return await UnitOfWork.Artists.GetArtistWithMusicsByIdAsync(id);
        }

        public async Task UpdateArtist(Artist updateArtist, Artist artist)
        {
            updateArtist.Name = artist.Name;
            await UnitOfWork.CommitAsync();
        }
    }
}
