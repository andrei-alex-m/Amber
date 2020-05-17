using Amber.Data.Utilities;
namespace Amber.Data.Model
{
    [BsonCollection("Maps")]
    public class Map: MongoDocument
    {
        public int[,] Points { get; set; }
    }
}
