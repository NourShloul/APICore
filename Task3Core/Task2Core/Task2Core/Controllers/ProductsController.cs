using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2Core.Models;

namespace Task2Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public ProductsController(MyDbContext db)
        {
            _db = db;
        }

        [Route("getAllProducts")]
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            //var products = _db.Products.ToList();
            var products = _db.Products.Select(b => new
            {
                b.ProductId,
                b.ProductName,
                b.Description,
                b.Price,
                b.CategoryId,
                b.ProductImage,
                b.Category
            });
            if (products == null)
                return NotFound("No products found.");

            return Ok(products);
        }

        [HttpGet("getAllproductByDes")]
        public IActionResult GetProductByDes() 
        {
            var product = _db.Products.OrderByDescending(b => b.Price).Select(b => new
            {
                b.ProductId,
                b.ProductName,
                b.Description,
                b.Price,
                b.CategoryId,
                b.ProductImage,
                b.Category
            });
            if (product == null)
                return NotFound("No products found.");

            return Ok(product);
        }
        [Route("GetProductById/{id}")]
        [HttpGet]
        public IActionResult GetProductById(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID.");
            var product = _db.Products.Find(id);
            if (product == null) return NotFound("Product not found.");

            return Ok(product);
        }

        [Route("GetProductByName/{name}")]
        [HttpGet]
        public IActionResult GetProductByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Name cannot be null or empty.");

            var product = _db.Products.FirstOrDefault(p => p.ProductName == name);
            if (product == null) return NotFound("Product not found.");
            return Ok(product);
        }

        [Route("DeleteProduct/{id}")]
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID.");

            var product = _db.Products.Find(id);
            if (product == null) return NotFound("Product not found.");

            _db.Products.Remove(product);
            _db.SaveChanges();

            return Ok("Product deleted.");
        }

        [Route("GetProductByCatId/{id:int}")]
        [HttpGet]
        public ActionResult GetProductByCatId(int id)
        {
            if (id <= 0) { return BadRequest(); }
            var x = _db.Products.Where(x => x.CategoryId == id).ToList();
            if (x == null)
            {
                return NotFound();
            }
            return Ok(x);
        }
    }
}
