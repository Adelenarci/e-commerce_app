using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourNamespace.Models;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // POST: api/Users/register
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(User user)
    {
        // Bu noktada şifreleme, validasyon gibi işlemler yapılabilir.
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.KullanıcıID }, user);
    }

    // POST: api/Users/login
    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(LoginModel loginModel)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginModel.Email && u.Şifre == loginModel.Password);

        if (user == null)
        {
            return Unauthorized("Invalid login credentials.");
        }

        // Burada bir JWT token veya session oluşturabilirsiniz.
        return Ok(user);
    }

    // POST: api/Users/logout
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // Token veya session'ı sona erdirme işlemi burada yapılabilir.
        return Ok("Logged out successfully.");
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.KullanıcıID)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
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

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.KullanıcıID == id);
    }
}
