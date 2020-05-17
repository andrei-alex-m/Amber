using Amber.Data.Enums;
using Amber.Data.Model;
using Amber.Data.Utilities;
using MongoDB.Driver;

namespace Amber.Data.Repo
{
    public class Seeder
    {
        IMongoRepository<Tank> _tankRepo;
        IMongoRepository<TankComponent> _tankComponentsRepo;
        IMongoRepository<TankComposition> _tankCompositionRepo;
        IMongoRepository<Map> _mapRepo;
        IAmberDatabaseSettings _settings;
        TankFactory tankFactory;
        public Seeder(IMongoRepository<Tank> tankRepo, IMongoRepository<TankComponent> tankComponentsRepo, IMongoRepository<TankComposition> tankCompositionRepo, IMongoRepository<Map> mapRepo, IAmberDatabaseSettings settings)
        {
            _tankRepo = tankRepo;
            _tankComponentsRepo = tankComponentsRepo;
            _tankCompositionRepo = tankCompositionRepo;
            _mapRepo = mapRepo;
            _settings = settings;

            tankFactory = new TankFactory(_tankRepo, _tankComponentsRepo, _tankCompositionRepo);

        }

        public async void Seed()
        {
            var database = new MongoClient(_settings.ConnectionString).GetDatabase(_settings.DatabaseName);

            //database already seeded
            if (database.ListCollections().ToList().Count > 0)
            {
                database.DropCollection("Tanks");
                database.DropCollection("TankComponents");
                database.DropCollection("TankCompositions");
                database.DropCollection("Maps");
            }


            database.CreateCollection("Tanks");
            database.CreateCollection("TankComponents");
            database.CreateCollection("TankCompositions");
            database.CreateCollection("Maps");

            await _tankComponentsRepo.InsertManyAsync(new TankComponent[]
            {
                new TankComponent()
                {
                    Name="Standard",
                    Side=Side.back|Side.front|Side.left|Side.right,
                    Armor=10,
                    ComponentType=ComponentType.body,
                    Weight=10
                },
                new TankComponent()
                {
                    Name="Reinforced",
                    ComponentType=ComponentType.body,
                    Side=Side.back|Side.front|Side.left|Side.right,
                    Armor=15,
                    Weight=15
                },
                new TankComponent
                {
                    Name="Standard",
                    ComponentType=ComponentType.sidearmor,
                    Side=Side.left|Side.right,
                    Armor=5,
                    Weight=5
                },
                new TankComponent
                {
                    Name="Reinforced",
                    ComponentType=ComponentType.sidearmor,
                    Side=Side.left|Side.right,
                    Armor=10,
                    Weight=10
                },
                new TankComponent
                {
                    Name="Standard",
                    ComponentType=ComponentType.sideskirt,
                    Side=Side.left|Side.right,
                    Armor=5,
                    Weight=5
                },
                new TankComponent
                {
                    Name="Reinforced",
                    ComponentType=ComponentType.sideskirt,
                    Side=Side.left|Side.right,
                    Armor=10,
                    Weight=10
                },
                new TankComponent
                {
                    Name="Standard",
                    ComponentType=ComponentType.frontplate,
                    Side=Side.front,
                    Armor=5,
                    Weight=5
                },
                new TankComponent
                {
                    Name="Reinforced",
                    ComponentType=ComponentType.frontplate,
                    Side=Side.front,
                    Armor=10,
                    Weight=10
                }

            });

            await _tankRepo.InsertManyAsync(new Tank[]
            {
                new Tank
                {
                    Name = "Jeffrey",
                    DamagePerShot=10,
                    MovesPerTurn=3,
                    Range=2
                },
                new Tank
                {
                    Name="Hillary",
                    DamagePerShot=20,
                    MovesPerTurn=2,
                    Range=5
                },
                new Tank
                {
                    Name="Harvey",
                    DamagePerShot=15,
                    MovesPerTurn=3,
                    Range=4
                }

            });

            await _tankCompositionRepo.InsertManyAsync(new TankComposition[]
            {
                new TankComposition
                {
                    Name="JeffreyComposition",
                    TankName="Jeffrey",
                    TankComponents=new ComponentTypeNamePair[]
                    {
                        new ComponentTypeNamePair {ComponentType=ComponentType.body, Name="Standard"},
                        new ComponentTypeNamePair {ComponentType=ComponentType.sidearmor, Name="Standard"},
                        new ComponentTypeNamePair {ComponentType=ComponentType.sideskirt, Name="Standard"},
                        new ComponentTypeNamePair {ComponentType=ComponentType.frontplate, Name="Standard"},
                    }
                },
                new TankComposition
                {
                    Name="HillaryComposition",
                    TankName="Hillary",
                    TankComponents=new ComponentTypeNamePair[]
                    {
                        new ComponentTypeNamePair {ComponentType=ComponentType.body, Name="Reinforced"},
                        new ComponentTypeNamePair {ComponentType=ComponentType.sidearmor, Name="Reinforced"},
                        new ComponentTypeNamePair {ComponentType=ComponentType.sideskirt, Name="Reinforced"},
                        new ComponentTypeNamePair {ComponentType=ComponentType.frontplate, Name="Reinforced"},
                    }
                },
                new TankComposition
                {
                    Name="HarveyComposition",
                    TankName="Harvey",
                    TankComponents=new ComponentTypeNamePair[]
                    {
                        new ComponentTypeNamePair {ComponentType=ComponentType.body, Name="Reinforced"},
                        new ComponentTypeNamePair {ComponentType=ComponentType.sidearmor, Name="Reinforced"},
                        new ComponentTypeNamePair {ComponentType=ComponentType.sideskirt, Name="Reinforced"},
                        new ComponentTypeNamePair {ComponentType=ComponentType.frontplate, Name="Reinforced"},
                    }
                }
            });

            await _mapRepo.InsertOneAsync(new Map
            {
                Name="Kursk",
                Points = new int[10,10]
                {
                    {0,1,1,0,0,0,0,1,0,0},
                    {0,1,1,0,0,1,0,0,0,0},
                    {0,0,0,0,0,0,0,0,0,0},
                    {0,0,0,1,0,0,1,1,0,0},
                    {0,0,1,0,0,0,0,0,0,0},
                    {1,0,0,0,0,1,0,0,1,1},
                    {0,0,0,0,1,1,0,0,0,0},
                    {1,0,0,0,0,0,0,1,0,0},
                    {0,0,1,0,0,1,0,0,0,0},
                    {0,0,0,1,0,0,0,1,0,0},
                }
            });


            //tank building
            _ = await tankFactory.BuildTankAsync("JeffreyComposition");
            _ = await tankFactory.BuildTankAsync("HillaryComposition");
            _ = await tankFactory.BuildTankAsync("HarveyComposition");


        }

    }
}
