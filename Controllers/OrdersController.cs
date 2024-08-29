using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        // GET: api/Orders
        // Tüm siparişleri listele
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                return await _context.Orders.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here as needed
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Orders/5
        // Belirli bir siparişi ID'sine göre getir
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

        // POST: api/Orders
        // Yeni bir sipariş oluştur
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            // Generate a unique random order ID
            Random rnd = new Random();
            int newOrderId;
            do
            {
                newOrderId = rnd.Next(1000, 9999);
            } while (_context.Orders.Any(o => o.SiparişId == newOrderId));

            order.SiparişId = newOrderId;

            // Set the order date to the current date and time
            order.SiparişTarihi = DateTime.Now;

            // Ensure the total amount is provided by the client (e.g., from cart.html)
            if (order.ToplamTutar == null)
            {
                return BadRequest("Total amount is required.");
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.SiparişId }, order);
        }

        // DELETE: api/Orders/5
        // Mevcut bir siparişi sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

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