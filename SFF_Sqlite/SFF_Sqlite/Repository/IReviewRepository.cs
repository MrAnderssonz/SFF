using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF_Sqlite.Models;
using SFF_Sqlite.DTO;

namespace SFF_Sqlite.Repository
{
    public interface IReviewRepository
    {
        Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews();
        Task<ActionResult<ReviewDto>> GetReview(int id);
        Task<ActionResult<ReviewDto>> PostNewReview(Review review);
        Task<ActionResult<ReviewDto>> DeleteTrivia(int id);
        Task<ActionResult<ReviewDto>> ChangeTrivia(int id, Review trivia);
    }
}
