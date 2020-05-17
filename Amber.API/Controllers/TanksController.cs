using Amber.Data.Model;
using Amber.Data.Repo;
using Microsoft.AspNetCore.Mvc;

namespace Amber.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TanksController : ControllerBase
    {
        private IMongoRepository<Tank> _tankRepo;
        public TanksController(IMongoRepository<Tank> tankRepo)
        {
            _tankRepo = tankRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _tankRepo.AsQueryable();
            return Ok(result);
        }

        [HttpGet("GetByNames")]
        public IActionResult GetByNames([FromQuery] string[] names)
        {
            var result = _tankRepo.FindManyByNames(names);
            return Ok(result);
        }
    }
}
