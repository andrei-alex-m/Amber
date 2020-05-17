using System.Linq;
using System.Threading.Tasks;
using Amber.Data.Model;
using Amber.Data.Repo;

namespace Amber.Data.Utilities
{
    public class TankFactory
    {
        IMongoRepository<Tank> _tankRepo;
        IMongoRepository<TankComponent> _tankComponentRepo;
        IMongoRepository<TankComposition> _tankCompositionRepo;
        public TankFactory(IMongoRepository<Tank> tankRepo, IMongoRepository<TankComponent> tankComponentRepo, IMongoRepository<TankComposition> tankCompositionRepo)
        {
            _tankRepo = tankRepo;
            _tankComponentRepo = tankComponentRepo;
            _tankCompositionRepo = tankCompositionRepo;
        }

        public async Task<Tank> BuildTankAsync(string compositionName)
        {
            var composition = await _tankCompositionRepo.FindByNameAsync( compositionName );
            var tank = await _tankRepo.FindByNameAsync(composition.TankName);
            tank.Components.Clear();
            foreach (var componentPair in composition.TankComponents)
            {
                var component = await _tankComponentRepo.FindOneAsync(x => x.ComponentType == componentPair.ComponentType && x.Name == componentPair.Name);
                tank.Components.Add(component);
            }

            tank.Armor = tank.Components.Sum(x => x.Armor);
            _tankRepo.ReplaceOne(tank);
            return tank;
        }
    }
}
