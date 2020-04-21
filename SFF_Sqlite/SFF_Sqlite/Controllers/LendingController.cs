using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF_Sqlite.Models;
using SFF_Sqlite.Repository;
using SFF_Sqlite.DTO;

namespace SFF_Sqlite.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class LendingController : ControllerBase
    {
        private readonly ILendingRepository _repository;

        public LendingController(ILendingRepository repository)
        {
            this._repository = repository;
        }

        // To see all leandings
        // /api/lending
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LendingDto>>> GetAllLendings()
        {

            return await _repository.GetAllLendings();

        }

        // To see a specific lending
        // /api/lending/1
        [HttpGet("{id}")]
        public async Task<ActionResult<LendingDto>> GetLending(int id)
        {
            return await _repository.GetLending(id);
        }

        // To make a new lending
        // /api/lending/new
        [HttpPost("new")]
        public async Task<ActionResult<LendingDto>> LendMovie(Lending lending)
        {
            var result = await _repository.LendMovie(lending);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }

        }

        // To return a lending
        // /api/lending/1/return
        [HttpPut("{id}/return")]
        public async Task<ActionResult<LendingDto>> ReturnMovie(int id)
        {
            return Ok(await _repository.ReturnMovie(id));

        }
    }
}
