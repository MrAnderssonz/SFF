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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _repository;

        public ReviewController(IReviewRepository repository)
        {
            this._repository = repository;
        }

        // To get all reviews
        // /api/review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews()
        {

            return Ok(await _repository.GetAllReviews());

        }

        // To get a specific review
        // /api/review/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {

            return Ok(await _repository.GetReview(id));

        }

        //To make a new review
        // /api/review/new
        [HttpPost("new")]
        public async Task<ActionResult<ReviewDto>> PostNewReview(Review review)
        {
            var result = await _repository.PostNewReview(review);

            if(result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }

        }

        // To delete the trivia in a review
        // /api/review/1/deletetrivia
        [HttpPut("{id}/deletetrivia")]
        public async Task<ActionResult<ReviewDto>> DeleteTrivia(int id)
        {

            return Ok(await _repository.DeleteTrivia(id));

        }

        // To change the trivia in a review
        // /api/review/1/changetrivia
        [HttpPut("{id}/changetrivia")]
        public async Task<ActionResult<ReviewDto>> ChangeTrivia(int id, Review trivia)
        {

            return Ok(await _repository.ChangeTrivia(id, trivia));

        }
    }
}