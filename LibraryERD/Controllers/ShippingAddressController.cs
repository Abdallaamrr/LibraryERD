using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryERD.Controllers
{
    public class ShippingAddressController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShippingAddressController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllShippingAddresses")]
        public ActionResult<ShippingAddresses> GetShippingAddresses()
        {
            var result = _context.ShippingAddress.ToList();
            return Ok(result);
        }

        [HttpGet("GetShippingAddressById/{id}")]
        public async Task<ActionResult<ShippingAddresses>> GetShippingAddressById(int id)
        {
            var shippingAddress = await _context.ShippingAddress.Where(e => e.CustomerId == id).FirstOrDefaultAsync();
            if (shippingAddress is null)
            {
                return NotFound("SHIPPING ADDRESS NOT FOUND");
            }
            return Ok(shippingAddress);
        }

        [HttpPost("AddShippingAddress")]
        public async Task<ActionResult<ShippingAddresses>> AddShippingAddress(ShippingAddresses shippingAddress)
        {
            await _context.ShippingAddress.AddAsync(shippingAddress);
            await _context.SaveChangesAsync();

            return Ok(shippingAddress);
        }


        [HttpPut("UpdateShippingAddress/{id}")]
        public async Task<ActionResult<ShippingAddresses>> UpdateShippingAddress(int id, ShippingAddresses updatedShippingAddress)
        {
            var shippingAddress = await _context.ShippingAddress.Where(e => e.CustomerId == id).FirstOrDefaultAsync();
            if (shippingAddress is null)
            {
                return NotFound("SHIPPING ADDRESS NOT FOUND");
            }
            shippingAddress.AddressLine = updatedShippingAddress.AddressLine;
            shippingAddress.City = updatedShippingAddress.City;
            shippingAddress.CustomerId = updatedShippingAddress.CustomerId;
            shippingAddress.PostalCode = updatedShippingAddress.PostalCode;

            await _context.SaveChangesAsync();
            return Ok(shippingAddress);
        }

        [HttpDelete("DeleteShippingAddress/{id}")]
        public async Task<ActionResult<ShippingAddresses>> DeleteShippingAddress(int id)
        {
            var shippingAddress = await _context.ShippingAddress.Where(e => e.CustomerId == id).FirstOrDefaultAsync();
            if (shippingAddress is null)
            {
                return NotFound("SHIPPING ADDRESS NOT FOUND");
            }
            _context.ShippingAddress.Remove(shippingAddress);
            await _context.SaveChangesAsync();
            return Ok(shippingAddress);
        }
    }
}
