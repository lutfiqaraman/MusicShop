using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MusicShop.Core.Models;
using System;

namespace MusicShop.Data.MongoDB.Setting
{
    public class DatabaseSettings : IDatabaseSettings
    {
        private readonly IMongoDatabase MongoDatabase;

        public DatabaseSettings(IOptions<Settings> options, IMongoClient mongoClient)
        {
            MongoDatabase = mongoClient.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Composer> Composers => MongoDatabase.GetCollection<Composer>("Composers");
    }
}
