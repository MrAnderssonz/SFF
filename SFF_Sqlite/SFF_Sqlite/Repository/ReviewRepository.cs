using System;
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
    public class ReviewRepository : IReviewRepository
    {
        private readonly MyDbContest _context;

        public ReviewRepository(MyDbContest context)
        {
            this._context = context;
        }

        // Method to get a list of all review
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews()
        {

             var reviewlist = await _context.Reviews
                                            .Include(r => r.Lending)
                                            .ThenInclude(re => re.FilmClub)
                                            .Include(r => r.Lending)
                                            .ThenInclude(re => re.Movie)
                                            .ToListAsync();

            var reviewDtoList = new List<ReviewDto>();

            foreach (var r in reviewlist)
            {
                var review = new ReviewDto
                {
                    Id = r.Id,
                    Movie = r.Lending.Movie.Title,
                    FilmClub = r.Lending.FilmClub.Name,
                    Grade = r.Grade,
                    Trivia = r.Trivia

                };

                reviewDtoList.Add(review);
            }
            return reviewDtoList;

        }

        // Method to get a specific review
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {

            var review = await _context.Reviews.FindAsync(id);
            var reviewDb = await _context.Lendings.Where(r => r.Id == review.LendingId)
                                                       .Include(r => r.Movie)
                                                       .Include(r => r.FilmClub)
                                                       .Include(r => r.Review)
                                                       .FirstOrDefaultAsync();
            var reviewDto = new ReviewDto
            {
                Id = reviewDb.Review.Id,
                Movie = reviewDb.Movie.Title,
                FilmClub = reviewDb.FilmClub.Name,
                Grade = reviewDb.Review.Grade,
                Trivia = reviewDb.Review.Trivia
            };

            return reviewDto;

        }

        // Method to post a new review
        public async Task<ActionResult<ReviewDto>> PostNewReview(Review review)
        {

            var oldReview = _context.Reviews.Where(r => r.LendingId == review.LendingId)
                                            .Count();
            if (oldReview == 0)
            {
                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();

                var mId = await _context.Lendings.Where(r => r.Id == review.LendingId)
                                                .FirstOrDefaultAsync();

                var movie = await _context.Movies.Where(m => m.Id == mId.MovieId)
                                                    .FirstOrDefaultAsync();

                var avgGrade = await _context.Reviews.Include(r => r.Lending)
                                                     .Where(r => r.Lending.MovieId == movie.Id)
                                                     .AverageAsync(r => r.Grade);
     

                movie.AverageGrade = Math.Round((decimal)avgGrade, 2);
                await _context.SaveChangesAsync();

                var reviewDb = await _context.Lendings.Where(r => r.Id == review.LendingId)
                                                       .Include(r => r.Movie)
                                                       .Include(r => r.FilmClub)
                                                       .Include(r => r.Review)
                                                       .FirstOrDefaultAsync();
                var reviewDto = new ReviewDto
                {
                    Id = reviewDb.Review.Id,
                    Movie = reviewDb.Movie.Title,
                    FilmClub = reviewDb.FilmClub.Name,
                    Grade = reviewDb.Review.Grade,
                    Trivia = reviewDb.Review.Trivia
                };

                return reviewDto;
            }
            else
            {
                var reviewDto = new ReviewDto();
                reviewDto = null;
                return reviewDto;
            }

        }

        // Method to delete a trivia in a review
        public async Task<ActionResult<ReviewDto>> DeleteTrivia(int id)
        {

            var review = await _context.Reviews.FindAsync(id);
            review.Trivia = null;

            await _context.SaveChangesAsync();

            var reviewDb = await _context.Lendings.Where(r => r.Id == review.LendingId)
                                                       .Include(r => r.Movie)
                                                       .Include(r => r.FilmClub)
                                                       .Include(r => r.Review)
                                                       .FirstOrDefaultAsync();
            var reviewDto = new ReviewDto
            {
                Id = reviewDb.Review.Id,
                Movie = reviewDb.Movie.Title,
                FilmClub = reviewDb.FilmClub.Name,
                Grade = reviewDb.Review.Grade,
                Trivia = reviewDb.Review.Trivia
            };

            return reviewDto;

        }

        // Method to change a trivia in a review
        public async Task<ActionResult<ReviewDto>> ChangeTrivia(int id, Review trivia)
        {

            var review = await _context.Reviews.FindAsync(id);
            review.Trivia = trivia.Trivia;

            await _context.SaveChangesAsync();

            var reviewDb = await _context.Lendings.Where(r => r.Id == review.LendingId)
                                                       .Include(r => r.Movie)
                                                       .Include(r => r.FilmClub)
                                                       .Include(r => r.Review)
                                                       .FirstOrDefaultAsync();
            var reviewDto = new ReviewDto
            {
                Id = reviewDb.Review.Id,
                Movie = reviewDb.Movie.Title,
                FilmClub = reviewDb.FilmClub.Name,
                Grade = reviewDb.Review.Grade,
                Trivia = reviewDb.Review.Trivia
            };

            return reviewDto;

        }

    }
}
