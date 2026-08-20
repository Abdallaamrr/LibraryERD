using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryERD.Controllers
{
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllCustomers")]
        public ActionResult<Customers> GetCustomers()
        {
            var result = _context.Customer.ToList();
            return Ok(result);
        }

        [HttpGet("GetCustomerById/{id}")]
        public async Task<ActionResult<Customers>> GetCustomerById(int id)
        {
            var customer = await _context.Customer.Where(e => e.CustomerId == id).FirstOrDefaultAsync();
            if (customer is null)
            {
                return NotFound("USER NOT FOUND");
            }
            return Ok(customer);
        }

        [HttpPost("AddCustomer")]
        public async Task<ActionResult<Customers>> AddCustomer(Customers customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }


        [HttpPut("UpdateCustomer/{id}")]
        public async Task<ActionResult<Customers>> UpdateCustomer(int id, Customers updatedCustomer)
        {
            var customer = await _context.Customer.Where(e => e.CustomerId == id).FirstOrDefaultAsync();
            if (customer is null)
            {
                return NotFound("USER NOT FOUND");
            }
            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;

            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<ActionResult<Customers>> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.Where(e => e.CustomerId == id).FirstOrDefaultAsync();
            if (customer is null)
            {
                return NotFound("USER NOT FOUND");
            }
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }
    }
}