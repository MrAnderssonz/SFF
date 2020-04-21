using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF_Sqlite.Models;
using SFF_Sqlite.DTO;

namespace SFF_Sqlite.Repository
{
    public interface IFilmClubRepository
    {
        Task<ActionResult<IEnumerable<FilmClubDto>>> GetAllFilmClubs();
        Task<ActionResult<FilmClubDto>> GetFilmClub(int id);
        Task<ActionResult<IEnumerable<FilmClubLendingDto>>> GetActiveFilmClubMovies(int id);
        Task<ActionResult<IEnumerable<FilmClubLendingDto>>> GetAllFilmClubMovies(int id);
        Task<ActionResult<FilmClubDto>> AddNewFilmClub(FilmClub club);
        Task<ActionResult<FilmClubDto>> MakeAFilmClubInActive(int id);
        Task<ActionResult<FilmClubDto>> ChangeFilmClubName(int id, FilmClub club);
        Task<ActionResult<FilmClubDto>> ChangeFilmClubLocation(int id, FilmClub club);
    }
}
