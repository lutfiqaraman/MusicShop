using MongoDB.Bson;
using MongoDB.Driver;
using MusicShop.Core.Models;
using MusicShop.Core.Repositories;
using MusicShop.Data.MongoDB.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Data.MongoDB.Repository
{
    public class ComposerRepository : IComposerRepository
    {
        private readonly IDatabaseSettings Context;

        public ComposerRepository(IDatabaseSettings context)
        {
            Context = context;
        }

        public async Task<Composer> Create(Composer composer)
        {
            await Context.Composers.InsertOneAsync(composer);
            return composer;
        }

        public async Task<bool> Delete(int id)
        {
            string convertedId = Convert.ToString(id);
            ObjectId objectId = new ObjectId(convertedId);

            FilterDefinition<Composer> filter = Builders<Composer>.Filter.Eq(m => m.Id, objectId);
            DeleteResult deleteResult = await Context.Composers.DeleteOneAsync(filter);
            bool result = deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

            return result;
        }

        public async Task<IEnumerable<Composer>> GetAllComposers()
        {
            return await Context.Composers.Find(_ => true).ToListAsync();
        }

        public async Task<Composer> GetComposerById(int id)
        {
            string convertedId = Convert.ToString(id);
            ObjectId objectId = new ObjectId(convertedId);

            FilterDefinition<Composer> filter = Builders<Composer>.Filter.Eq(m => m.Id, objectId);
            return await Context.Composers.Find(filter).FirstOrDefaultAsync();
        }

        public void Update(int id, Composer composer)
        {
            string convertedId = Convert.ToString(id);
            ObjectId objectId = new ObjectId(convertedId);

            FilterDefinition<Composer> filter = Builders<Composer>.Filter.Eq(m => m.Id, objectId);
            UpdateDefinition<Composer> update = Builders<Composer>
                .Update
                .Set("FirstName", composer.FirstName)
                .Set("LastName", composer.LastName);

            Context.Composers.FindOneAndUpdate(filter, update);
        }
    }
}
