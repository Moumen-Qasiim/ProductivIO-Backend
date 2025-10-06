using ProductivIOBackend.Models;
using ProductivIOBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ProductivIOBackend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UserController(AppDbContext db) => _db = db;

        // GET : /api/User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _db.Users
                .Where(u => u.Email.Contains(""))
                .ToListAsync();

            return Ok(users);
        }
    }

}