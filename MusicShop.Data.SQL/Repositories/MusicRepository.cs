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
    public class MusicRepository : Repository<Music>, IMusicRepository
    {
        private MusicStoreDbContext DbContext
        {
            get { return Context as MusicStoreDbContext; }
        }

        public MusicRepository(MusicStoreDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Music>> GetAllMusicsWithArtistByArtistIdAsync(int artistId)
        {
            return await DbContext.Musics
                .Include(m => m.Artist)
                .Where(m => m.ArtistId == artistId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Music>> GetAllMusicsWithArtistAsync()
        {
            return await DbContext.Musics
                .Include(m => m.Artist)
                .ToListAsync();
        }

        public async Task<Music> GetMusicByIdWithArtisAsync(int id)
        {
            return await DbContext.Musics
                .Include(m => m.Artist)
                .SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}
