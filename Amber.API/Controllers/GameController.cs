using System.Collections.Concurrent;
using System.Threading.Tasks;
using Amber.Data.DTO;
using Amber.Infra;
using Microsoft.AspNetCore.Mvc;

namespace Amber.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController:ControllerBase
    {
        private GameService _gameService;
        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("PlayMultiple")]
        public IActionResult PlayMultiple([FromQuery] GameRequestDTO[] games)
        {
            var results = new ConcurrentBag<GameResponseDTO>();

            Parallel.ForEach(games, async g => {
                var summary = await _gameService.GameOn(g);
                results.Add(new GameResponseDTO(g) { Summary = summary });
            });
            return Ok(results);
        }

        [HttpGet("PlaySingle")]
        public IActionResult PlaySingle([FromQuery] GameRequestDTO game)
        {
            var summary =  _gameService.GameOn(game).Result;
            var result = new GameResponseDTO(game) { Summary = summary };
            return Ok(result);
        }
    }
}
