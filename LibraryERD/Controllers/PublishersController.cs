using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryERD.Controllers
{
    public class PublishersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PublishersController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllPublishers")]
        public ActionResult<Publishers> GetPublishers()
        {
            var result = _context.Publisher.ToList();
            return Ok(result);
        }

        [HttpGet("GetPublisherById/{id}")]
        public async Task<ActionResult<Publishers>> GetPublisherById(int id)
        {
            var publisher = await _context.Publisher.Where(e => e.PublisherId == id).FirstOrDefaultAsync();
            if (publisher is null)
            {
                return NotFound("PUBLISHER NOT FOUND");
            }
            return Ok(publisher);
        }

        [HttpPost("AddPublisher")]
        public async Task<ActionResult<Publishers>> AddPublisher(Publishers publisher)
        {
            await _context.Publisher.AddAsync(publisher);
            await _context.SaveChangesAsync();

            return Ok(publisher);
        }


        [HttpPut("UpdatePublisher")]
        public async Task<ActionResult<Publishers>> UpdatePublisher(Publishers updatedPublisher)
        {
            var publisher = await _context.Publisher.Where(e => e.PublisherId == updatedPublisher.PublisherId).FirstOrDefaultAsync();
            if (publisher is null)
            {
                return NotFound("PUBLISHER NOT FOUND");
            }
            publisher.PublisherName = updatedPublisher.PublisherName;
            publisher.PublisherCountry = updatedPublisher.PublisherCountry;

            await _context.SaveChangesAsync();
            return Ok(publisher);
        }

        [HttpDelete("DeletePublisher/{id}")]
        public async Task<ActionResult<Publishers>> DeletePublisher(int id)
        {
            var publisher = await _context.Publisher.Where(e => e.PublisherId == id).FirstOrDefaultAsync();
            if (publisher is null)
            {
                return NotFound("PUBLISHER NOT FOUND");
            }
            _context.Publisher.Remove(publisher);
            await _context.SaveChangesAsync();
            return Ok(publisher);
        }
    }
}
