using System;
namespace Amber.Data
{
    public class AmberDatabaseSettings : IAmberDatabaseSettings
    {
        public string MapsCollectionName { get; set; }
        public string TanksCollectionName { get; set; }
        public string TankComponentsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IAmberDatabaseSettings
    {
        string MapsCollectionName { get; set; }
        string TanksCollectionName { get; set; }
        string TankComponentsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
