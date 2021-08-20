using MusicShop.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace MusicShop.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IMusicRepository Musics { get; }
        IArtistRepository Artists { get;  }
        Task<int> CommitAsync();
    }
}
