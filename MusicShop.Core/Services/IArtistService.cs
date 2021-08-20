using MusicShop.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Core.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetAllArtists();
        Task<Artist> GetArtistById(int id);
        Task<Music> CreateArtist(Artist artist);
        Task UpdateArtist(Artist updateArtist, Artist artist);
        Task DeleteArtist(Artist artist);
    }
}
