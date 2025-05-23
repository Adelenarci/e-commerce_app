using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using E_Ticaret_Uygulaması.Models;

namespace E_Ticaret_Uygulaması.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly SmartprodatabaseContext _context;

        public AdminController(SmartprodatabaseContext context)
        {
            _context = context;
        }

        // GET last product (GET /api/Admin/products/last)
[HttpGet("products/last")]
public async Task<ActionResult<Product>> GetLastProduct()
{
    var lastProduct = await _context.Products.OrderByDescending(p => p.ÜrünId).FirstOrDefaultAsync();
    if (lastProduct == null)
    {
        return NotFound("No products found.");
    }
    return lastProduct;
}

        // Ürün Ekleme (POST /api/Admin/products)
        [HttpPost("products")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduct), new { id = product.ÜrünId }, product);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }

        // Admin Logout (POST /api/Admin/logout)
        [HttpPost("logout")]
          public IActionResult LogoutAdmin()
            {
            return Ok(new { success = true, message = "Logged out successfully" });
            }           

        // Ürün Düzenleme (PUT /api/Admin/products/{id})
        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ÜrünId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Admin Login (POST /api/Admin/login)
        [HttpPost("login")]
        public IActionResult LoginAdmin([FromBody] User model)
        {
          
            var user = _context.Users.SingleOrDefault(u => u.KullanıcıAdı == model.KullanıcıAdı);
            if (user == null || user.Rol != "Admin")
            {
                return BadRequest(new { success = false, message = "Invalid username or password or not authorized" });
            }

            if (user.Şifre != model.Şifre)
            {
                return BadRequest(new { success = false, message = "Invalid username or password" });
            }

            return Ok(new { success = true, role = user.Rol, username = user.KullanıcıAdı, userId = user.KullanıcıId });
        }


        // Ürün Silme (DELETE /api/Admin/products/{id})
        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Stok Yönetimi (PUT /api/Admin/products/{id}/stock)
        [HttpPut("products/{id}/stock")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] int stock)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Stok = stock;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Ürün Bilgisi Getirme (GET /api/Admin/products/{id})
        [HttpGet("products/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ÜrünId == id);
        }
    }
}
