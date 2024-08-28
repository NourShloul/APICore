using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task5.DTO;
using Task5.Models;

namespace Task5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CategoryController(MyDbContext db)
        {
            _db = db;
        }

        [Route("getAllCategories")]
        [HttpGet]
        public IActionResult getAllCategories()
        {

            var categories = _db.Categories.ToList();

            return Ok(categories);
        }

        [Route("getCategoryById/{id}")]
        [HttpGet]
        public IActionResult getCategoryById(int id)
        {
            var category = _db.Categories.Find(id);

            return Ok(category);
        }

        [Route("getCategoryByName/{name}")]
        [HttpGet]
        public IActionResult getCategoryByName(string name)
        {
            var category = _db.Categories.FirstOrDefault(model => model.CategoryName == name);

            return Ok(category);
        }

        [Route("DeleteCategory/{id}")]
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var delproducts = _db.Products.Where(e => e.CategoryId == id).ToList();
            _db.Products.RemoveRange(delproducts);
            _db.SaveChanges();


            var delCategory = _db.Categories.Where(e => e.CategoryId == id).FirstOrDefault();
            if (delCategory != null)
            {
                _db.Categories.Remove(delCategory);
                _db.SaveChanges();
                return Ok($"Category deleted successfully.");
            }
            return NotFound($"Category not found.");
        }

        [HttpPost("AddCategory")]
        public IActionResult addCategory([FromForm] CategoryRequsetDTO category)
        {
            if (category.CategoryImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var imagePath = Path.Combine(uploadsFolderPath, category.CategoryImage.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    category.CategoryImage.CopyToAsync(stream);
                }

            }

            var newCategory = new Category()
            {
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage.FileName,
            };

            _db.Categories.Add(newCategory);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut("UpdateCategory/{id}")]
        public IActionResult UpdateCategory(int id, [FromForm] CategoryRequsetDTO category)
        {

            if (category.CategoryImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var imagePath = Path.Combine(uploadsFolderPath, category.CategoryImage.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    category.CategoryImage.CopyTo(stream);
                }

            }

            var Updatecategory = _db.Categories.Find(id);

            Updatecategory.CategoryName = category.CategoryName;
            Updatecategory.CategoryImage = category.CategoryImage.FileName;

            _db.Categories.Update(Updatecategory);
            _db.SaveChanges();
            return Ok();
        }
    }
}
