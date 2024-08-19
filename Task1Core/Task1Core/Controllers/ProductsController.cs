using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1Core.Models;

namespace Task1Core.Controllers
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

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var product = _db.Products.Select(b => new
            {
                b.ProductId,
                b.ProductName,
                b.Description,
                b.Price,
                b.CategoryId,
                b.ProductImage,
                b.Category
            });
            return Ok(product);
        }
        [HttpGet("id")]
        public IActionResult GetProductById(int id)
        {
            var product = _db.Products.Where(c => c.ProductId == id).FirstOrDefault();
            return Ok(product);
        }
    }
}
