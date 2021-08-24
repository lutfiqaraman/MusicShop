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

        public async Task<Composer> Create(Composer composer)
        {
            return await Context.Create(composer);
        }

        public async Task<bool> Delete(int id)
        {
            return await Context.Delete(id);
        }

        public async Task<IEnumerable<Composer>> GetAllComposers()
        {
            return await Context.GetAllComposers();
        }

        public async Task<Composer> GetComposerById(int id)
        {
            return await Context.GetComposerById(id);
        }

        public void Update(int id, Composer composer)
        {
            Context.Update(id, composer);
        }
    }
}
