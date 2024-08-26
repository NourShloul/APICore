using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task2Core.Models;

namespace Task2Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db;

        public UsersController(MyDbContext db)
        {
            _db = db;
        }
        [Route("getAllUsers")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _db.Users.ToList();
            if (users == null || !users.Any())
                return NotFound("No users found.");

            return Ok(users);
        }

        [Route("GetUserById/{id:int}")]
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID.");

            var user = _db.Users.Find(id);
            if (user == null) return NotFound("User not found.");

            return Ok(user);
        }

        [Route("GetUserByName/{name}")]
        [HttpGet]
        public IActionResult GetUserByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Name cannot be null or empty.");

            var user = _db.Users.FirstOrDefault(u => u.Username == name);
            if (user == null) return NotFound("User not found.");

            return Ok(user);
        }

        [Route("DeleteUser/{id:int}")]
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID.");

            var user = _db.Users.Find(id);
            if (user == null) return NotFound("User not found.");

            _db.Users.Remove(user);
            _db.SaveChanges();

            return Ok("User deleted.");
        }
    }
}
