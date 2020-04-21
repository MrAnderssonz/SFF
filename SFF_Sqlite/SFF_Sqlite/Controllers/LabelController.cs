using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF_Sqlite.Models;
using SFF_Sqlite.Repository;

namespace SFF_Sqlite.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelRepository _repository;
        

        public LabelController(ILabelRepository repository)
        {
            this._repository = repository;
        }

        // To get a label for a lending for Posten
        // /api/label/1
        [HttpGet("{id}")]
        [Produces("application/xml")]
        public async Task<ActionResult<Label>> GetLabel(int id)
        {
            return await _repository.GetLabel(id);

        }
    }
}
