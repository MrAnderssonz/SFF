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
    public class MovieController : ControllerBase
    {
        readonly IMovieRepository _repository;
        public MovieController(IMovieRepository repository)
        {
            this._repository = repository;
        }

        // To see all movies
        // /api/movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies()
        {

            return Ok(await _repository.GetAllMovies());

        }

        // To see a specific movie
        // /api/movie/1
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {

            return Ok(await _repository.GetMovie(id));

        }

        // To add a new movie to the system
        // /api/movie/new
        [HttpPost("new")]
        public async Task<ActionResult<MovieDto>> AddNewMovie(Movie movie)
        {

            return Ok(await _repository.AddNewMovie(movie));
        }

        // To change the max amont a movie can be lended at the same time
        // /api/movie/1/maxlending
        [HttpPut("{id}/maxlending")]
        public async Task<ActionResult<MovieDto>> ChangeMaxLending(int id, Movie newValue)
        {
            var movie = await _repository.ChangeMaxLending(id, newValue);

            return Ok(movie);
        }
    }
}
