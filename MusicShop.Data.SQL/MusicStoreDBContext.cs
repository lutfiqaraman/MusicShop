using Microsoft.EntityFrameworkCore;
using MusicShop.Core.Models;
using MusicShop.Data.SQL.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Data.SQL
{
    public class MusicStoreDbContext : DbContext
    {
        public DbSet<Music>  Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }

        public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new MusicConfiguration());

            builder
                .ApplyConfiguration(new ArtistConfiguration());
        }

    }
}
