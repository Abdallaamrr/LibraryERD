using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryERD.Controllers
{
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllCategories")]
        public ActionResult<Categories> GetCategories()
        {
            var result = _context.Category.ToList();
            return Ok(result);
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<Categories>> GetCategoryById(int id)
        {
            var category = await _context.Category.Where(e => e.CategoryId == id).FirstOrDefaultAsync();
            if (category is null)
            {
                return NotFound("CATEGORY NOT FOUND");
            }
            return Ok(category);
        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult<Categories>> AddCategory(Categories category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }


        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<Categories>> UpdateCategory(Categories updatedCategory)
        {
            var category = await _context.Category.Where(e => e.CategoryId == updatedCategory.CategoryId).FirstOrDefaultAsync();
            if (category is null)
            {
                return NotFound("CATEGORY NOT FOUND");
            }
            category.CategoryName = updatedCategory.CategoryName;

            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult<Categories>> DeleteCategory(int id)
        {
            var category = await _context.Category.Where(e => e.CategoryId == id).FirstOrDefaultAsync();
            if (category is null)
            {
                return NotFound("CATEGORY NOT FOUND");
            }
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }
    }
}