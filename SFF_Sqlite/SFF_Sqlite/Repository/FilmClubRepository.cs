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
    public class FilmClubRepository : IFilmClubRepository
    {
        private readonly MyDbContest _context;

        public FilmClubRepository(MyDbContest context)
        {
            this._context = context;
        }

        // Method to get a list of all filmclubs from database
        public async Task<ActionResult<IEnumerable<FilmClubDto>>> GetAllFilmClubs()
        {
            var filmClubList = await _context.FilmClubs
                                             .Where(f => f.IsActive == true)
                                             .ToListAsync();
            var FilmClubDtoList = new List<FilmClubDto>();

            foreach (var fc in filmClubList)
            {
                var filmclub = new FilmClubDto
                {
                    Id = fc.Id,
                    Name = fc.Name,
                    City = fc.City
                };

                FilmClubDtoList.Add(filmclub);

            }

            return FilmClubDtoList;
        }

        // Method to get a spcific filmclub
        public async Task<ActionResult<FilmClubDto>> GetFilmClub(int id)
        {
            var filmclubDb = await _context.FilmClubs
                                            .Where(f => f.Id == id)
                                            .FirstOrDefaultAsync();

            var filmclubDto = new FilmClubDto
            {
                Id = filmclubDb.Id,
                Name = filmclubDb.Name,
                City = filmclubDb.City
            };

            return filmclubDto;

        }

        // Method to get all of a spcific filmclubs movies they lended
        public async Task<ActionResult<IEnumerable<FilmClubLendingDto>>> GetAllFilmClubMovies(int id)
        {
            var lendingList = await _context.Lendings
                                         .Where(r => r.FilmClubId == id)
                                         .Include(r => r.Movie)
                                         .ToListAsync();

            var lendingDtoList = new List<FilmClubLendingDto>();

            foreach (var l in lendingList)
            {
                var lending = new FilmClubLendingDto
                {
                    LendingId = l.Id,
                    Movie = l.Movie.Title,
                    LendingDate = l.LendingDate
                };

                lendingDtoList.Add(lending);
            }

            return lendingDtoList;

        }

        // Method to get all of a specifit filmclub movies that they curent lending
        public async Task<ActionResult<IEnumerable<FilmClubLendingDto>>> GetActiveFilmClubMovies(int id)
        {
            var lendingList = await _context.Lendings
                                         .Where(r => r.FilmClubId == id && r.IsActive == true)
                                         .Include(r => r.Movie)
                                         .ToListAsync();

            var ledingDtoList = new List<FilmClubLendingDto>();

            foreach (var l in lendingList)
            {
                var lending = new FilmClubLendingDto
                {
                    LendingId = l.Id,
                    Movie = l.Movie.Title,
                    LendingDate = l.LendingDate
                };

                ledingDtoList.Add(lending);
            }

            return ledingDtoList;

        }

        // Method to add a new filmclub
        public async Task<ActionResult<FilmClubDto>> AddNewFilmClub(FilmClub filmClub)
        {

            await _context.FilmClubs.AddAsync(filmClub);
            await _context.SaveChangesAsync();

            var filmClubDto = new FilmClubDto
            {
                Id = filmClub.Id,
                Name = filmClub.Name,
                City = filmClub.City
            };

            return filmClubDto;

        }

        // Method to make a filmclub inactive
        public async Task<ActionResult<FilmClubDto>> MakeAFilmClubInActive(int id)
        {
            var filmClub = await _context.FilmClubs.Where(f => f.Id == id)
                                                   .FirstOrDefaultAsync();

            filmClub.IsActive = false;
            await _context.SaveChangesAsync();

            var filmClubDto = new FilmClubDto
            {
                Id = filmClub.Id,
                Name = filmClub.Name,
                City = filmClub.City
            };

            return filmClubDto;
        }

        // Method to change the name of a filmclub
        public async Task<ActionResult<FilmClubDto>> ChangeFilmClubName(int id, FilmClub filmClub)
        {
            var filmClubDb = await _context.FilmClubs.Where(f => f.Id == id)
                                                     .FirstOrDefaultAsync();

            filmClubDb.Name = filmClub.Name;
            await _context.SaveChangesAsync();

            var filmClubDto = new FilmClubDto
            {
                Id = filmClubDb.Id,
                Name = filmClubDb.Name,
                City = filmClubDb.City
            };

            return filmClubDto;
        }

        // Method to change the location of a filmclub
        public async Task<ActionResult<FilmClubDto>> ChangeFilmClubLocation(int id, FilmClub filmClub)
        {
            var filmClubDb = await _context.FilmClubs.Where(f => f.Id == id)
                                                     .FirstOrDefaultAsync();


            filmClubDb.City = filmClub.City;
            await _context.SaveChangesAsync();

            var filmClubDto = new FilmClubDto
            {
                Id = filmClubDb.Id,
                Name = filmClubDb.Name,
                City = filmClubDb.City
            };

            return filmClubDto;
        }
    }
}