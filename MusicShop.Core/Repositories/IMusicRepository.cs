using MusicShop.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Core.Repositories
{
    public interface IMusicRepository : IRepository<Music>
    {
        Task<IEnumerable<Music>> GetAllMusicsWithArtistAsync();
        Task<Music> GetMusicByIdWithArtisAsync(int id);
        Task<IEnumerable<Music>> GetAllMusicsWithArtistByArtistIdAsync(int artistId);
    }
}
