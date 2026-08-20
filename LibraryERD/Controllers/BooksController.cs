using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryERD.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllBooks")]
        public ActionResult<Books> GetBooks()
        {
            var result = _context.Book.ToList();
            return Ok(result);
        }

        [HttpGet("GetBookById/{id}")]
        public async Task<ActionResult<Books>> GetBookById(int id)
        {
            var book = await _context.Book.Where(e => e.BookId == id).FirstOrDefaultAsync();
            if (book is null)
            {
                return NotFound("BOOK NOT FOUND");
            }
            return Ok(book);
        }

        [HttpPost("AddBook")]
        public async Task<ActionResult<Books>> AddBook(Books book)
        {
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }


        [HttpPut("UpdateBook")]
        public async Task<ActionResult<Books>> UpdateBook(Books updatedBook)
        {
            var book = await _context.Book.Where(e => e.BookId == updatedBook.BookId).FirstOrDefaultAsync();
            if (book is null)
            {
                return NotFound("BOOK NOT FOUND");
            }
            book.price = updatedBook.price;
            book.BookTitle = updatedBook.BookTitle;
            book.ISBN = updatedBook.ISBN;
            book.PublishedDate = updatedBook.PublishedDate;
            book.PublisherId = updatedBook.PublisherId;

            await _context.SaveChangesAsync();
            return Ok(book);
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<ActionResult<Books>> DeleteBook(int id)
        {
            var book = await _context.Book.Where(e => e.BookId == id).FirstOrDefaultAsync();
            if (book is null)
            {
                return NotFound("BOOK NOT FOUND");
            }
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return Ok(book);
        }
    }
}
