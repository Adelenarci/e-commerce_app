using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Ticaret_Uygulaması.Models;

namespace E_Ticaret_Uygulaması.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly SmartprodatabaseContext _context;

        public OrdersController(SmartprodatabaseContext context)
        {
            _context = context;
        }
       public class OrderRequest
        {
         public Order Order { get; set; }
         public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();     
        }

        // GET: api/Orders
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                return await _context.Orders.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // GET: api/Orders/GetOrderDetails/5
       
        [HttpGet("GetOrderDetails/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails(int orderId)
        {
            var orderDetails = await _context.OrderDetails
                                              .Where(od => od.SiparişId == orderId)
                                              .ToListAsync();

            if (orderDetails == null || orderDetails.Count == 0)
            {
                return NotFound("No details found for this order.");
            }

            return Ok(orderDetails);    
        }


// POST: api/Orders
[HttpPost]
public async Task<ActionResult<Order>> PostOrder([FromBody] OrderRequest orderRequest)
{
    var order = orderRequest.Order;
    var orderDetails = orderRequest.OrderDetails;

   
    Random rnd = new Random();
    int newOrderId;
    do
    {
        newOrderId = rnd.Next(1000, 9999);
    } while (_context.Orders.Any(o => o.SiparişId == newOrderId));

    order.SiparişId = newOrderId;
    order.SiparişTarihi = DateTime.Now;

    if (order.ToplamTutar == null)
    {
        return BadRequest("Total amount is required.");
    }

    _context.Orders.Add(order);
    await _context.SaveChangesAsync();

   
    int maxDetailId = await _context.OrderDetails.MaxAsync(d => (int?)d.SiparişDetayId) ?? 0;
    int nextDetailId = maxDetailId + 1;

   
    foreach (var detail in orderDetails)
    {
        detail.SiparişDetayId = nextDetailId++;
        detail.SiparişId = newOrderId;
        _context.OrderDetails.Add(detail);
    }

    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetOrder), new { id = order.SiparişId }, order);
}
        
        // Delete an existing order
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails.Where(od => od.SiparişId == id).ToListAsync();

            _context.OrderDetails.RemoveRange(orderDetails);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.SiparişId == id);
        }
    }
}