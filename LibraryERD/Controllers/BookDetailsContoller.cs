using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryERD.Controllers
{
    public class BookDetailsContoller : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookDetailsContoller(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllBookDetails")]
        public ActionResult<BookDetails> GetBookDetails()
        {
            var result = _context.BookDetail.ToList();
            return Ok(result);
        }

        [HttpGet("GetBookDetailById/{id}")]
        public async Task<ActionResult<BookDetails>> GetBookDetailById(int id)
        {
            var bookDetail = await _context.BookDetail.Where(e => e.BookId == id).FirstOrDefaultAsync();
            if (bookDetail is null)
            {
                return NotFound("BOOK DETAIL NOT FOUND");
            }
            return Ok(bookDetail);
        }

        [HttpPost("AddBookDetail")]
        public async Task<ActionResult<BookDetails>> AddBookDetail(BookDetails bookDetail)
        {
            await _context.BookDetail.AddAsync(bookDetail);
            await _context.SaveChangesAsync();

            return Ok(bookDetail);
        }


        [HttpPut("UpdateBookDetail/{id}")]
        public async Task<ActionResult<BookDetails>> UpdateBookDetail(int id, BookDetails updatedBookDetail)
        {
            var bookDetail = await _context.BookDetail.Where(e => e.BookId == id).FirstOrDefaultAsync();
            if (bookDetail is null)
            {
                return NotFound("BOOK DETAIL NOT FOUND");
            }
            bookDetail.Language = updatedBookDetail.Language;
            bookDetail.PageCount = updatedBookDetail.PageCount;
            bookDetail.Description = updatedBookDetail.Description;

            await _context.SaveChangesAsync();
            return Ok(bookDetail);
        }

        [HttpDelete("DeleteBookDetail/{id}")]
        public async Task<ActionResult<BookDetails>> DeleteBookDetail(int id)
        {
            var bookDetail = await _context.BookDetail.Where(e => e.BookId == id).FirstOrDefaultAsync();
            if (bookDetail is null)
            {
                return NotFound("BOOK DETAIL NOT FOUND");
            }
            _context.BookDetail.Remove(bookDetail);
            await _context.SaveChangesAsync();
            return Ok(bookDetail);
        }
    }
}
