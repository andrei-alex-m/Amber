using System.Collections.Generic;
using Amber.Data.Enums;
using Amber.Data.Utilities;

namespace Amber.Data.Model
{
    [BsonCollection("TankCompositions")]
    public class TankComposition:MongoDocument
    {
        public string TankName { get; set; }
        public ICollection<ComponentTypeNamePair> TankComponents { get; set; }
    }

    public class ComponentTypeNamePair
    {
        public ComponentType ComponentType;
        public string Name;
    }
}
