using MusicShop.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Core.Repositories
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<IEnumerable<Artist>> GetAllArtistsWithMusicsAsync();
        Task<Artist> GetArtistWithMusicsByIdAsync(int id);
    }
}
