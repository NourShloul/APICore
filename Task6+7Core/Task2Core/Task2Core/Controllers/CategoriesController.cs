using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2Core.DTOfolder;
using Task2Core.Models;

namespace Task2Core.Controllers
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

        [Route("getAllCategories")]
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _db.Categories.ToList();
            if (categories == null)
                return NotFound("No categories found.");

            return Ok(categories);
        }

        [Route("GetCategoryById/{id:int:min(5)}")]
        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID.");

            var category = _db.Categories.Find(id);
            if (category == null) return NotFound("Category not found.");

            return Ok(category);
        }

        [Route("GetCategoryByName/{name}")]
        [HttpGet]
        public IActionResult GetCategoryByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Name cannot be null or empty.");

            var category = _db.Categories.FirstOrDefault(c => c.CategoryName == name);
            if (category == null) return NotFound("Category not found.");

            return Ok(category);
        }

        [Route("DeleteCategory/{id:int}")]
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID.");

            var category = _db.Categories.Find(id);
            if (category == null) return NotFound("Category not found.");

            _db.Categories.Remove(category);
            _db.SaveChanges();

            return Ok("Category deleted.");
        }
        //task 4
        [HttpPost]
        public IActionResult createCategory( [FromForm] categoryRequestDTO category)
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(uploadFolder)) 
            {
                Directory.CreateDirectory(uploadFolder);
            }
            var imgFile = Path.Combine(uploadFolder,category.CategoryImage.FileName);
            using(var stream = new FileStream(imgFile, FileMode.Create))
            {
                category.CategoryImage.CopyToAsync(stream);
            }
            var c = new Category
            {
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage.FileName,
            };
            _db.Categories.Add(c);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult updateCategory(int id,[FromForm] categoryRequestDTO category) 
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            var imgFile = Path.Combine(uploadFolder, category.CategoryImage.FileName);
            using (var stream = new FileStream(imgFile, FileMode.Create))
            {
                category.CategoryImage.CopyToAsync(stream);
            }

            var c = _db.Categories.FirstOrDefault(n => n.CategoryId == id);
            c.CategoryName = category.CategoryName;
            c.CategoryImage = category.CategoryImage.FileName;

            _db.Categories.Update(c);
            _db.SaveChanges();
            return Ok();
        }

    }
}
