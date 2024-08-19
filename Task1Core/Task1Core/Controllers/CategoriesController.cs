using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1Core.Models;

namespace Task1Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _db;
        public CategoriesController(MyDbContext db)
        {
                _db = db;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var category = _db.Categories.ToList();
            return Ok(category);
        }
        [HttpGet("id")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _db.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
            return Ok(category);
        }
    }
}
