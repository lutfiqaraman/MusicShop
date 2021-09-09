using MusicShop.Core;
using MusicShop.Core.Repositories;
using MusicShop.Data.SQL.Repositories;
using System.Threading.Tasks;

namespace MusicShop.Data.SQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicStoreDbContext Context;
        private IMusicRepository MusicRepository;
        private IArtistRepository ArtistRepository;
        private IUserRepository UserRepository;

        public UnitOfWork(MusicStoreDbContext context)
        {
            Context = context;
        }

        public IMusicRepository Musics => MusicRepository ??= new MusicRepository(Context);

        public IArtistRepository Artists => ArtistRepository ??= new ArtistRepository(Context);

        public IUserRepository Users => UserRepository ??= new UserRepository(Context);

        public async Task<int> CommitAsync()
        {
            int result = await Context.SaveChangesAsync();
            return result;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
