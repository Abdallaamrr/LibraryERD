using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryERD.Controllers
{
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllOrders")]
        public ActionResult<Orders> GetOrders()
        {
            var result = _context.Order.ToList();
            return Ok(result);
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<ActionResult<Orders>> GetOrderById(int id)
        {
            var order = await _context.Order.Where(e => e.OrderId == id).FirstOrDefaultAsync();
            if (order is null)
            {
                return NotFound("ORDER NOT FOUND");
            }
            return Ok(order);
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult<Orders>> AddOrder(Orders order)
        {
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }


        [HttpPut("UpdateOrder/{id}")]
        public async Task<ActionResult<Orders>> UpdateOrder(int id, Orders updatedOrder)
        {
            var order = await _context.Order.Where(e => e.OrderId == id).FirstOrDefaultAsync();
            if (order is null)
            {
                return NotFound("ORDER NOT FOUND");
            }
            order.CustomerId = updatedOrder.CustomerId;
            order.PostalCode = updatedOrder.PostalCode;
            

            await _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<ActionResult<Orders>> DeleteOrder(int id)
        {
            var order = await _context.Order.Where(e => e.OrderId == id).FirstOrDefaultAsync();
            if (order is null)
            {
                return NotFound("ORDER NOT FOUND");
            }
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }
    }
}

