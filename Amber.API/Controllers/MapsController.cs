using Amber.Data.Model;
using Amber.Data.Repo;
using Microsoft.AspNetCore.Mvc;

namespace Amber.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController: ControllerBase
    {
        private IMongoRepository<Map> _mapRepo;
        public MapsController(IMongoRepository<Map> mapRepo)
        {
            _mapRepo = mapRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _mapRepo.AsQueryable();
            return Ok(result);
        }

        [HttpGet("GetByNames")]
        public IActionResult GetByNames([FromQuery] string[] names)
        {
            var result = _mapRepo.FindManyByNames(names);
            return Ok(result);
        }
    }
}
