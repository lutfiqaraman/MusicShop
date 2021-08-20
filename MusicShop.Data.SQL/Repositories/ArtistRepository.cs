using Microsoft.EntityFrameworkCore;
using MusicShop.Core.Models;
using MusicShop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Data.SQL.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        private MusicStoreDbContext DbContext
        {
            get { return Context as MusicStoreDbContext; }
        }

        public ArtistRepository(MusicStoreDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Artist>> GetAllArtistsWithMusicsAsync()
        {
            List<Artist> result = await DbContext.Artists
                .Include(a => a.Musics)
                .ToListAsync();

            return result;
        }

        public async Task<Artist> GetArtistWithMusicsByIdAsync(int id)
        {
            Artist result = await DbContext.Artists
                .Include(a => a.Musics)
                .SingleOrDefaultAsync(a => a.Id == id);

            return result;
        }
    }
}
