using MusicShop.Core.Models;
using MusicShop.Core.Repositories;
using MusicShop.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Services.Services
{
    public class ComposerService : IComposerService
    {
        private readonly IComposerRepository Context;

        public ComposerService(IComposerRepository context)
        {
            Context = context;
        }

        public Task<Composer> Create(Composer composer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Composer>> GetAllComposers()
        {
            throw new NotImplementedException();
        }

        public Task<Composer> GetComposerById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Composer composer)
        {
            throw new NotImplementedException();
        }
    }
}
