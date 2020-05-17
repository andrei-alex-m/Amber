using System.Collections.Generic;
using Amber.Data.Utilities;


namespace Amber.Data.Model
{
    [BsonCollection("Tanks")]
    public class Tank: MongoDocument
    {
        public int MovesPerTurn { get; set; }
        public int DamagePerShot { get; set; }
        public int Range { get; set; }
        public int Armor { get; set; }
        public ICollection<TankComponent> Components { get; set; }

        public Tank()
        {
            Components = new List<TankComponent>();
        }
    }
}
