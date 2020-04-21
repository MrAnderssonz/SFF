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
    public class LendingRepository : ILendingRepository
    {
        private readonly MyDbContest _context;

        public LendingRepository(MyDbContest context)
        {
            this._context = context;
        }

        // Method to get a list of all lendings
        public async Task<ActionResult<IEnumerable<LendingDto>>> GetAllLendings()
        {

            var lendinglist = await _context.Lendings
                                         .Include(m => m.Movie)
                                         .Include(f => f.FilmClub)
                                         .ToListAsync();

            var lendingDtoList = new List<LendingDto>();

            foreach (var r in lendinglist)
            {
                var rent = new LendingDto
                {
                    Id = r.Id,
                    LendingDate = r.LendingDate,
                    Movie = r.Movie.Title,
                    FilmStudio = r.FilmClub.Name

                };

                lendingDtoList.Add(rent);

            }

            return lendingDtoList;

        }

        // Method to get a specific lending
        public async Task<ActionResult<LendingDto>> GetLending(int id)
        {
            var lending = await _context.Lendings
                                         .Where(r => r.Id == id)
                                         .Include(m => m.Movie)
                                         .Include(f => f.FilmClub)
                                         .FirstOrDefaultAsync();

            var lendingDto = new LendingDto
            {
                Id = lending.Id,
                LendingDate = lending.LendingDate,
                Movie = lending.Movie.Title,
                FilmStudio = lending.FilmClub.Name

            };

            return lendingDto;

        }

        // Method to lend a movie
        public async Task<ActionResult<LendingDto>> LendMovie(Lending lending)
        {
            var movie = await _context.Movies.FindAsync(lending.MovieId);

            var moviesOut = await _context.Lendings.Where(r => r.MovieId == lending.MovieId)
                                                .Where(r => r.IsActive)
                                                .CountAsync();

            if (movie.MaxLending > moviesOut)
            {
                await _context.Lendings.AddAsync(lending);
                await _context.SaveChangesAsync();


                var lendingDb = await _context.Lendings
                                         .Where(r => r.Id == lending.Id)
                                         .Include(m => m.Movie)
                                         .Include(f => f.FilmClub)
                                         .FirstOrDefaultAsync();

                var lendingDto = new LendingDto
                {
                    Id = lending.Id,
                    LendingDate = lendingDb.LendingDate,
                    Movie = lendingDb.Movie.Title,
                    FilmStudio = lendingDb.FilmClub.Name

                };

                return lendingDto;
            }
            else
            {
                var lendFail = new LendingDto();
                lendFail = null;

                return lendFail;
            }


        }

        // Method to return a movie
        public async Task<ActionResult<LendingDto>> ReturnMovie(int id)
        {
            var lending = await _context.Lendings
                                         .Where(r => r.Id == id)
                                         .Include(m => m.Movie)
                                         .Include(f => f.FilmClub)
                                         .FirstOrDefaultAsync();

            lending.IsActive = false;

            await _context.SaveChangesAsync();

            var lendingDto = new LendingDto
            {
                Id = lending.Id,
                LendingDate = lending.LendingDate,
                Movie = lending.Movie.Title,
                FilmStudio = lending.FilmClub.Name

            };

            return lendingDto;
        }
    }
}
