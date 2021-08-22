using MongoDB.Driver;
using MusicShop.Core.Models;

namespace MusicShop.Data.MongoDB.Setting
{
    public interface IDatabaseSettings
    {
        IMongoCollection<Composer> Composers { get;  }
    }
}
