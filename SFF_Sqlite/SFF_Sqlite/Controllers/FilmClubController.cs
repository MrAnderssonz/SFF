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
    public class FilmClubController : ControllerBase
    {
        private readonly IFilmClubRepository _repository;

        public FilmClubController(IFilmClubRepository repository)
        {
            this._repository = repository;
        }

        // To see all filmclubs
        // /api/filmclub 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmClubDto>>> GetAllFilmClubs()
        {

            return Ok(await _repository.GetAllFilmClubs());
        }

        // To see a specific filmclub
        // /api/filmclub/1
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<FilmClubDto>>> GetFilmClub(int id)
        {

            return Ok(await _repository.GetFilmClub(id));
        }

        // To see a specific filmclubs lendings
        // /api/filmclub/1/lendings
        [HttpGet("{id}/lendings")]
        public async Task<ActionResult<IEnumerable<FilmClubLendingDto>>> GetAllFilmClubMovies(int id)
        {

            return Ok(await _repository.GetAllFilmClubMovies(id));
        }

        // To see a specific filmclubs active list of lendings
        // /api/filmclub/1/lendings/activelist
        [HttpGet("{id}/lendings/activelist")]
        public async Task<ActionResult<IEnumerable<FilmClubLendingDto>>> GetActiveFilmClubMovies(int id)
        {

            return Ok(await _repository.GetActiveFilmClubMovies(id));
        }

        // To create a new filmclub
        // /api/filmclub/new
        [HttpPost("new")]
        public async Task<ActionResult<FilmClubDto>> AddNewFilmClub(FilmClub club)
        {

            return Ok(await _repository.AddNewFilmClub(club));
        }

        // To make a filmclub inactive
        // /api/filmclub/2/delete
        [HttpPut("{id}/delete")]
        public async Task<ActionResult<FilmClubDto>> MakeAFilmClubInActive(int id)
        {

            return Ok(await _repository.MakeAFilmClubInActive(id));

        }

        // To change the name of a filmclub
        // /api/filmclub/1/changename
        [HttpPut("{id}/changename")]
        public async Task<ActionResult<FilmClubDto>> ChangeFilmClubName(int id, FilmClub club)
        {

            return Ok(await _repository.ChangeFilmClubName(id, club));

        }

        // To change the location of a filmclub
        // /api/filmclub/1/changelocation
        [HttpPut("{id}/changelocation")]
        public async Task<ActionResult<FilmClubDto>> ChangeFilmClubLocation(int id, FilmClub club)
        {
            return Ok(await _repository.ChangeFilmClubLocation(id, club));
        }
    }
}
