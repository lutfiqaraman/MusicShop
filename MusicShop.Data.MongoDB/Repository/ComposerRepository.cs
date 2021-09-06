using MongoDB.Bson;
using MongoDB.Driver;
using MusicShop.Core.Models;
using MusicShop.Core.Repositories;
using MusicShop.Data.MongoDB.Setting;
using System;
using System.Collections.Generic;
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

        public async Task<bool> Delete(string id)
        {
            ObjectId objectId = new ObjectId(id);

            FilterDefinition<Composer> filter = Builders<Composer>.Filter.Eq(m => m.Id, objectId);
            DeleteResult deleteResult = await Context.Composers.DeleteOneAsync(filter);
            bool result = deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

            return result;
        }

        public async Task<IEnumerable<Composer>> GetAllComposers()
        {
            return await Context.Composers.Find(_ => true).ToListAsync();
        }

        public async Task<Composer> GetComposerById(string id)
        {
            ObjectId objectId = new ObjectId(id);

            FilterDefinition<Composer> filter = Builders<Composer>.Filter.Eq(m => m.Id, objectId);
            return await Context.Composers.Find(filter).FirstOrDefaultAsync();
        }

        public void Update(string id, Composer composer)
        {
            ObjectId objectId = new ObjectId(id);

            FilterDefinition<Composer> filter = Builders<Composer>.Filter.Eq(m => m.Id, objectId);
            UpdateDefinition<Composer> update = Builders<Composer>
                .Update
                .Set("FirstName", composer.FirstName)
                .Set("LastName", composer.LastName);

            Context.Composers.FindOneAndUpdate(filter, update);
        }
    }
}
