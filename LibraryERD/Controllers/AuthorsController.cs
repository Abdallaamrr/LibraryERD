using LibraryERD.Domain;
using Microsoft.AspNetCore.Mvc;
using LibraryERD.Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace LibraryERD.Controllers
{
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllAuthors")]
        public ActionResult<Authors> GetAuthors()
        {
            var result = _context.Author.ToList();
            return Ok(result);
        }

        [HttpGet("GetAuthorById/{id}")]
        public async Task<ActionResult<Authors>> GetAuthorById(int id)
        {
            var author =await _context.Author.Where(e => e.AuthorId == id).FirstOrDefaultAsync();
            if (author is null)
            {
                return NotFound("USER NOT FOUND");
            }
            return Ok(author);
        }

        [HttpPost("AddAuthor")]
        public async Task<ActionResult<Authors>> AddAuthor(Authors author)
        {

            await _context.Author.AddAsync(author);
            await _context.SaveChangesAsync();

            return Ok(author);
        }


        [HttpPut("UpdateAuthor")]
        public async Task<ActionResult<Authors>> UpdateAuthor(Authors author)
        {
            var updatedAuthor = await _context.Author.Where(e => e.AuthorId == author.AuthorId).FirstOrDefaultAsync();
            if (updatedAuthor is null)
            {
                return NotFound("USER NOT FOUND");
            }
            updatedAuthor.FirstName = author.FirstName;
            updatedAuthor.LastName = author.LastName;

            await _context.SaveChangesAsync();
            return Ok(updatedAuthor);
        }

        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<ActionResult<Authors>> DeleteAuthor(int id)
        {
            var author = await _context.Author.Where(e => e.AuthorId == id).FirstOrDefaultAsync();
            if (author is null)
            {
                return NotFound("USER NOT FOUND");
            }
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return Ok(author);
        }
    }
}
