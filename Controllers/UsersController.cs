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
    public class UsersController : ControllerBase
    {
        private readonly SmartprodatabaseContext _context;

        public UsersController(SmartprodatabaseContext context)
        {
            _context = context;
        }

       //Register a new user
    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] User model)
    {
        // Check if the username already exists
        var existingUser = _context.Users.SingleOrDefault(u => u.KullanıcıAdı == model.KullanıcıAdı);
        if (existingUser != null)
        {
            return BadRequest(new { success = false, message = "Username already exists" });
        }

        // Get the last KullanıcıID and increment it
        int newUserId = 1;
        if (_context.Users.Any())
        {
            newUserId = _context.Users.Max(u => u.KullanıcıId) + 1;
        }

        // Create a new user with the next KullanıcıID
        var user = new User
        {
            KullanıcıId = newUserId,
            KullanıcıAdı = model.KullanıcıAdı,
            Şifre = model.Şifre, // No encryption applied
            Email = model.Email,
            Rol = "User" // Set role as 'User'
        };

        // Save to the database
        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(new { success = true, message = "User created successfully" });
    }

    // User login
    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] User model)
    {
        // Find the user by username
        var user = _context.Users.SingleOrDefault(u => u.KullanıcıAdı == model.KullanıcıAdı);
        if (user == null)
        {
            return BadRequest(new { success = false, message = "Invalid username or password" });
        }

        // Verify the password (plain text comparison)
        if (user.Şifre != model.Şifre)
        {
            return BadRequest(new { success = false, message = "Invalid username or password" });
        }

        return Ok(new { success = true, role = user.Rol, username = user.KullanıcıAdı });
    }

    // User logout (simple implementation)
    [HttpPost("logout")]
    public IActionResult LogoutUser()
    {
        // Perform any necessary logout operations, like token invalidation
        return Ok(new { success = true, message = "User logged out successfully" });
    }
    }
}
