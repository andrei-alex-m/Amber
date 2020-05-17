using MongoDB.Bson;

namespace Amber.Data.Model
{
    public abstract class MongoDocument:IMongoDocument
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }
    }
}
