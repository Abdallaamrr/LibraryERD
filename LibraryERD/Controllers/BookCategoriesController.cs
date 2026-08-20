using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryERD.Controllers
{
    public class BookCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookCategoriesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllBookCategories")]
        public ActionResult<BookCategories> GetBookCategories()
        {
            var result = _context.BookCategory.ToList();
            return Ok(result);
        }

        [HttpGet("GetBookCategoryById/{CategoryId}/Book/{BookId}")]
        public async Task<ActionResult<BookCategories>> GetBookCategoryById(int CategoryId, int BookId)
        {
            var bookCategory = await _context.BookCategory.Where(e => e.CategoryId == CategoryId && e.BookId == BookId).FirstOrDefaultAsync();
            if (bookCategory is null)
            {
                return NotFound("BOOK CATEGORY NOT FOUND");
            }
            return Ok(bookCategory);
        }

        [HttpPost("AddBookCategory")]
        public async Task<ActionResult<BookCategories>> AddBookCategory(BookCategories bookCategory)
        {
            await _context.BookCategory.AddAsync(bookCategory);
            await _context.SaveChangesAsync();

            return Ok(bookCategory);
        }


        [HttpPut("UpdateBookCategory")]
        public async Task<ActionResult<BookCategories>> UpdateBookCategory(BookCategories updatedBookCategory)
        {
            var bookCategory = await _context.BookCategory.Where(e => e.CategoryId == updatedBookCategory.CategoryId && e.BookId == updatedBookCategory.BookId).FirstOrDefaultAsync();
            if (bookCategory is null)
            {
                return NotFound("BOOK CATEGORY NOT FOUND");
            }
            bookCategory.CategoryId = updatedBookCategory.CategoryId;
            bookCategory.BookId = updatedBookCategory.BookId;

            await _context.SaveChangesAsync();
            return Ok(bookCategory);
        }

        [HttpDelete("DeleteBookCategory/{CategoryId}/Book/{BookId}")]
        public async Task<ActionResult<BookCategories>> DeleteBookCategory(int CategoryId, int BookId)
        {
            var bookCategory = await _context.BookCategory.Where(e => e.CategoryId == CategoryId && e.BookId == BookId).FirstOrDefaultAsync();
            if (bookCategory is null)
            {
                return NotFound("BOOK CATEGORY NOT FOUND");
            }
            _context.BookCategory.Remove(bookCategory);
            await _context.SaveChangesAsync();
            return Ok(bookCategory);
        }
    }
}
