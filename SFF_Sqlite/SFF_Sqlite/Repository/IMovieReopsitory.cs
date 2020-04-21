using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF_Sqlite.Models;
using SFF_Sqlite.DTO;

namespace SFF_Sqlite.Repository
{
    public interface IMovieRepository
    {
        Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies();
        Task<ActionResult<MovieDto>> GetMovie(int id);
        Task<ActionResult<MovieDto>> AddNewMovie(Movie movie);
        Task<ActionResult<MovieDto>> ChangeMaxLending(int id, Movie newValue);
    }
}
