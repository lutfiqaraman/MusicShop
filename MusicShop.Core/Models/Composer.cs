using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MusicShop.Core.Models
{
    public class Composer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
