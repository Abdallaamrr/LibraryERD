using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibraryERD.Controllers
{
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllReviews")]
        public ActionResult<Reviews> GetReviews()
        {
            var result = _context.Review.ToList();
            return Ok(result);
        }

        [HttpGet("GetReviewById/{CustomerId}/book/{BookId}")]
        public async Task<ActionResult<Reviews>> GetReviewById(int CustomerId, int BookId)
        {
            var review = await _context.Review.Where(e => e.CustomerId == CustomerId && e.BookId == BookId).FirstOrDefaultAsync();
            if (review is null)
            {
                return NotFound("REVIEW NOT FOUND");
            }
            return Ok(review);
        }

        [HttpPost("AddReview")]
        public async Task<ActionResult<Reviews>> AddReview(Reviews review)
        {
            await _context.Review.AddAsync(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }


        [HttpPut("UpdateReview/{CustomerId}/book/{BookId}")]
        public async Task<ActionResult<Reviews>> UpdateReview(int CustomerId, int BookId, Reviews updatedReview)
        {
            var review = await _context.Review.Where(e => e.CustomerId == CustomerId && e.BookId == BookId).FirstOrDefaultAsync();
            if (review is null)
            {
                return NotFound("REVIEW NOT FOUND");
            }
            review.Comment = updatedReview.Comment;
            review.Rating = updatedReview.Rating;
            review.ReviewDate = updatedReview.ReviewDate;
            review.CustomerId = updatedReview.CustomerId;
            review.BookId = updatedReview.BookId;

            await _context.SaveChangesAsync();
            return Ok(review);
        }

        [HttpDelete("DeleteReview/{CustomerId}/book/{BookId}")]
        public async Task<ActionResult<Reviews>> DeleteReview(int CustomerId, int BookId)
        {
            var review = await _context.Review.Where(e => e.CustomerId == CustomerId && e.BookId == BookId).FirstOrDefaultAsync();
            if (review is null)
            {
                return NotFound("REVIEW NOT FOUND");
            }
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return Ok(review);
        }
    }
}