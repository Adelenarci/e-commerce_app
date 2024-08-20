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
    public class ProductsController : ControllerBase
    {
        private readonly SmartprodatabaseContext _context;

        public ProductsController(SmartprodatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Products
        // Tüm ürünleri listele
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here as needed
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Products/5
        // Belirli bir ürünü ID'sine göre getir
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/search?name=productName
        // Ürün arama (isim ile)
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string name)
        {
            return await _context.Products
                .Where(p => p.İsim.Contains(name))
                .ToListAsync();
        }
    }
}
