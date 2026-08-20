using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryERD.Controllers
{
    public class OrderItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderItemsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllOrderItems")]
        public ActionResult<OrderItems> GetOrderItems()
        {
            var result = _context.OrderItem.ToList();
            return Ok(result);
        }

        [HttpGet("GetOrderItemById/{OrderId}/book/{BookId}")]
        public async Task<ActionResult<OrderItems>> GetOrderItemById(int OrderId, int BookId)
        {
            var orderItem = await _context.OrderItem.Where(e => e.OrderId == OrderId && e.BookId == BookId).FirstOrDefaultAsync();
            if (orderItem is null)
            {
                return NotFound("Order Item NOT FOUND");
            }
            return Ok(orderItem);
        }

        [HttpPost("AddOrderItem")]
        public async Task<ActionResult<OrderItems>> AddOrderItem(OrderItems orderItem)
        {
            await _context.OrderItem.AddAsync(orderItem);
            await _context.SaveChangesAsync();

            return Ok(orderItem);
        }


        [HttpPut("UpdateOrderItem/{OrderId}/book/{BookId}")]
        public async Task<ActionResult<OrderItems>> UpdateOrderItem(int OrderId, int BookId, OrderItems updatedOrderItem)
        {
            var orderItem = await _context.OrderItem.Where(e => e.OrderId == OrderId && e.BookId == BookId).FirstOrDefaultAsync();
            if (orderItem is null)
            {
                return NotFound("Order Item NOT FOUND");
            }
            orderItem.UnitPrice = updatedOrderItem.UnitPrice;
            orderItem.Quantity = updatedOrderItem.Quantity;
            orderItem.OrderId = updatedOrderItem.OrderId;
            orderItem.BookId = updatedOrderItem.BookId;
            await _context.SaveChangesAsync();
            return Ok(orderItem);
        }

        [HttpDelete("DeleteOrderItem{OrderId}/book/{BookId}")]
        public async Task<ActionResult<OrderItems>> DeleteOrderItem(int OrderId, int BookId)
        {
            var orderItem = await _context.OrderItem.Where(e => e.OrderId == OrderId && e.BookId == BookId).FirstOrDefaultAsync();
            if (orderItem is null)
            {
                return NotFound("Order Item NOT FOUND");
            }
            _context.OrderItem.Remove(orderItem);
            await _context.SaveChangesAsync();
            return Ok(orderItem);
        }
    }
}
