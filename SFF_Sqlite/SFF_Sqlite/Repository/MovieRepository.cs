using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFF_Sqlite.Context;
using SFF_Sqlite.Models;
using SFF_Sqlite.DTO;

namespace SFF_Sqlite.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MyDbContest _context;

        public MovieRepository(MyDbContest context)
        {
            this._context = context;
        }

        // Method to get a list of all movies
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies()
        {
            var movieList = await _context.Movies.ToListAsync();
            var movieDtoList = new List<MovieDto>();

            foreach (var m in movieList)
            {
                var movie = new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    AverageGrade = m.AverageGrade,
                    MaxLending = m.MaxLending
                };

                movieDtoList.Add(movie);

            }
            
            return movieDtoList;
        }

        // Method to get a specific movie
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {

            var movie = await _context.Movies.Where(m => m.Id == id)
                                        .FirstOrDefaultAsync();

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                AverageGrade = movie.AverageGrade,
                MaxLending = movie.MaxLending
            };

            return movieDto;

        }

        // Method to add a new movie
        public async Task<ActionResult<MovieDto>> AddNewMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                AverageGrade = movie.AverageGrade,
                MaxLending = movie.MaxLending
            };

            return movieDto;
        }

        // Method to change the maximum amount a movie can be lended at the same time
        public async Task<ActionResult<MovieDto>> ChangeMaxLending(int id, Movie newValue)
        {
            var movie = await _context.Movies.FindAsync(id);
            movie.MaxLending = newValue.MaxLending;
            await _context.SaveChangesAsync();

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                AverageGrade = movie.AverageGrade,
                MaxLending = movie.MaxLending
            };

            return movieDto;
        }

    }
}
