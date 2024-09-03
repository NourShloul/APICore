using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Task2Core.DTOfolder;
using Task2Core.Models;

namespace Task2Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db;
        private readonly TokenGenerator _tokenGenerator;
        public UsersController(MyDbContext db, TokenGenerator tokenGenerator)
        {
            _db = db;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromForm] UserDTO newUser)
        {
            //if (newUser.Password != newUser.ConfirmPassword) 
            //{
            //    return BadRequest("Password not matching with Confirmpassword");
            //}

            byte[] passwordHash, passwordSalt;
            PasswordHasher.CreatePasswordHash(newUser.Password, out passwordHash, out passwordSalt);
            User user = new User
            {
                Username = newUser.Username,
                Email = newUser.Email,
                Password = newUser.Password,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,  
            };
            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok(new { message = "User added successfully.", user });
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm] UserDTO user)
        {
            var log = _db.Users.FirstOrDefault(x => x.Email == user.Email);//var log = _db.Users.Where(x => x.Email == user.Email).FirstOrDefault();
            if (user == null || !PasswordHasher.VerifyPassword(user.Password, log.PasswordHash, log.PasswordSalt))
            { 
                return Unauthorized("Invalid login"); 
            }
            var roles = _db.UserRoles.Where(x => x.UserId == log.UserId).Select(ur => ur.Role).ToList();
            var token = _tokenGenerator.GenerateToken(log.Username, roles);
            return Ok(new { Token = token});
            
        }


        //[Route("getAllUsers")]
        //[HttpGet]
        //public IActionResult GetAllUsers()
        //{
        //    var users = _db.Users.ToList();
        //    if (users == null || !users.Any())
        //        return NotFound("No users found.");

        //    return Ok(users);
        //}

        //[Route("GetUserById/{id:int}")]
        //[HttpGet]
        //public IActionResult GetUserById(int id)
        //{
        //    if (id <= 0) return BadRequest("Invalid ID.");

        //    var user = _db.Users.Find(id);
        //    if (user == null) return NotFound("User not found.");

        //    return Ok(user);
        //}

        [Route("GetUserByName/{name}")]
        [HttpGet]
        public IActionResult GetUserByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Name cannot be null or empty.");

            var user = _db.Users.FirstOrDefault(u => u.Username == name);
            if (user == null) return NotFound("User not found.");

            return Ok(user);
        }

        //[Route("DeleteUser/{id:int}")]
        //[HttpDelete]
        //public IActionResult DeleteUser(int id)
        //{
        //    if (id <= 0) return BadRequest("Invalid ID.");

        //    var user = _db.Users.Find(id);
        //    if (user == null) return NotFound("User not found.");

        //    _db.Users.Remove(user);
        //    _db.SaveChanges();

        //    return Ok("User deleted.");
        //}
        ////task 4
        //[HttpPost]
        //public IActionResult createUser([FromForm] userRequestDTO user)
        //{
        //    var c = new User
        //    {
        //        Username = user.Username,
        //        Password = user.Password,
        //        Email = user.Email,
        //    };
        //    _db.Users.Add(c);
        //    _db.SaveChanges();
        //    return Ok();
        //}
        //[HttpPut("{id}")]
        //public IActionResult updateUser(int id, [FromForm] userRequestDTO user)
        //{
        //    var c = _db.Users.FirstOrDefault(n => n.UserId == id);
        //    c.Username = user.Username;
        //    c.Password = user.Password;
        //    c.Email = user.Email;
        //    _db.SaveChanges();
        //    return Ok();
        //}
    }
}
