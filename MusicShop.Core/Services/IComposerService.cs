using MusicShop.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Core.Services
{
    public interface IComposerService
    {
        Task<IEnumerable<Composer>> GetAllComposers();
        Task<Composer> GetComposerById(int id);
        Task<Composer> Create(Composer composer);
        Task<bool> Delete(int id);
        void Update(int id, Composer composer);
    }
}
