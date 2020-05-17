using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amber.Data.DTO;
using Amber.Data.Model;
using Amber.Data.Repo;
using Amber.Logic;

namespace Amber.Infra
{
    public class GameService
    {
        IMongoRepository<Tank> _tankRepo;
        IMongoRepository<Map> _mapRepo;

        public GameService(IMongoRepository<Tank> tankRepo, IMongoRepository<Map> mapRepo)
        {
            _tankRepo = tankRepo;
            _mapRepo = mapRepo;
        }

        public async Task<string> GameOn(GameRequestDTO request)
        {
            var sb = new StringBuilder();

            var tanks = await _tankRepo.FindManyByNamesAsync(new string[] { request.TankOneName, request.TankTwoName });
            var tankOne = tanks.First();
            var tankTwo = tanks.Skip(1).First();

            var map = await _mapRepo.FindByNameAsync(request.MapName);

            //GameOn
            sb.AppendLine($"Game started between {tankOne.Name} and {tankTwo.Name} on map {map.Name}");

            sb.AppendLine($"{tankOne.Name} starts at x:{request.TankOnePos[0]},y:{request.TankOnePos[1]}");
            sb.AppendLine($"{tankTwo.Name} starts at x:{request.TankTwoPos[0]},y:{request.TankTwoPos[0]}");

            while (tankOne.Armor>=0 && tankTwo.Armor>=0)
            {
                var dist = Dist(request.TankOnePos, request.TankTwoPos);

                tankOne.Shoot(dist, tankTwo, sb);
                tankTwo.Shoot(dist, tankOne, sb);

                request.TankOnePos =  tankOne.Move(request.TankOnePos, request.TankTwoPos, dist, map, sb);
                request.TankTwoPos = tankTwo.Move(request.TankTwoPos, request.TankOnePos, dist, map, sb);
            }

            var winner = tankOne.Armor > 0 ? tankOne : tankTwo;
            var loser = winner.Equals(tankOne) ? tankTwo : tankOne;

            sb.AppendLine($"{winner.Name} did away with {loser.Name} winning the match on map {map.Name}");

            return sb.ToString();
        }

        public static double Dist(int[] one, int[] two)
        {
            //quicker than Math.Pow
            return Math.Sqrt((one[0] - two[0])* (one[0] - two[0]) + (one[1] - two[1])* (one[1] - two[1]));
        }
    }

    public static class TankExtensions
    {
        public static void Shoot(this Tank tank, double dist, Tank theOtherTank, StringBuilder sb)
        {
            //is it kill?
            if (dist <= tank.Range && tank.Armor >= 0)
            {
                theOtherTank.Armor -= tank.DamagePerShot;
                sb.AppendLine($"{tank.Name} shoots leaving {theOtherTank.Name} with {theOtherTank.Armor} armor");
            }
        }

        public static int[] Move (this Tank tank, int[] thisPosition, int[] theOtherPosition, double dist, Map map, StringBuilder sb )
        {
            //did it dieded?
            if (dist > tank.Range && tank.Armor >= 0)
            {
                var path = new Astar(map.Points, thisPosition, theOtherPosition, "Diagonal");
                //removes last position as they cant ovelap
                path.result.RemoveAt(path.result.Count - 1);
                //removes initial position
                path.result.RemoveAt(0);

                //does it reach the vicinity of the opposing tank?
                var toIndex = Math.Min(tank.MovesPerTurn, path.result.Count)-1;
                thisPosition = path.result[toIndex];

                sb.AppendLine($"{tank.Name} moves to x:{thisPosition[0]},y:{thisPosition[1]}");
            }
            else
            {
                if (tank.Armor >=0)
                    sb.AppendLine($"{tank.Name} doesnt bother to move");
                else
                    sb.AppendLine($"{tank.Name} is crippled");
            }

            return thisPosition;
        }
    }
}
