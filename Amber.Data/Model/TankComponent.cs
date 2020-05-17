using Amber.Data.Enums;
using Amber.Data.Utilities;

namespace Amber.Data.Model
{
    [BsonCollection("TankComponents")]
    public class TankComponent: MongoDocument
    {
        public ComponentType ComponentType { get; set; }
        public int Armor { get; set; }
        public Side Side { get; set; }
        public int Weight { get; set; }
    }
}
