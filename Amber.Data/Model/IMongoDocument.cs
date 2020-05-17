using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Amber.Data.Model
{
    public interface IMongoDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
