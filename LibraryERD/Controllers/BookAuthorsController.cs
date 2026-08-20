using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LibraryERD.Domain
{
    public class BookAuthorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookAuthorController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllBookAuthors")]
        public ActionResult<BookAuthors> GetBookAuthors()
        {
            var result = _context.BookAuthor.ToList();
            return Ok(result);
        }

        [HttpGet("GetBookAuthorById/{BookId}/Author/{AuthorId}")]
        public async Task<ActionResult<BookAuthors>> GetBookAuthorById(int BookId, int AuthorId)
        {
            var bookAuthor = await _context.BookAuthor.Where(e => e.AuthorId == AuthorId && e.BookId == BookId).FirstOrDefaultAsync();
            if (bookAuthor is null)
            {
                return NotFound("BOOK AUTHOR NOT FOUND");
            }
            return Ok(bookAuthor);
        }

        [HttpPost("AddBookAuthor")]
        public async Task<ActionResult<BookAuthors>> AddBookAuthor(BookAuthors bookAuthor)
        {
            await _context.BookAuthor.AddAsync(bookAuthor);
            await _context.SaveChangesAsync();

            return Ok(bookAuthor);
        }


        [HttpPut("UpdateBookAuthor/{BookId}/Author/{AuthorId}")]
        public async Task<ActionResult<BookAuthors>> UpdateBookAuthor(int BookId, int AuthorId, BookAuthors updatedBookAuthor)
        {
            var bookAuthor = await _context.BookAuthor.Where(e => e.AuthorId == AuthorId && e.BookId == BookId).FirstOrDefaultAsync();
            if (bookAuthor is null)
            {
                return NotFound("BOOK AUTHOR NOT FOUND");
            }
            bookAuthor.AuthorId = updatedBookAuthor.AuthorId;
            bookAuthor.BookId = updatedBookAuthor.BookId;

            await _context.SaveChangesAsync();
            return Ok(bookAuthor);
        }

        [HttpDelete("DeleteBookAuthor/{BookId}/Author/{AuthorId}")]
        public async Task<ActionResult<BookAuthors>> DeleteBookAuthor(int BookId, int AuthorId)
        {
            var bookAuthor = await _context.BookAuthor.Where(e => e.AuthorId == AuthorId && e.BookId == BookId).FirstOrDefaultAsync();
            if (bookAuthor is null)
            {
                return NotFound("BOOK AUTHOR NOT FOUND");
            }
            _context.BookAuthor.Remove(bookAuthor);
            await _context.SaveChangesAsync();
            return Ok(bookAuthor);
        }
    }
}
