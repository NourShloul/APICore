using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task5.DTO;
using Task5.Models;

namespace Task5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDbContext _db;

        public ProductController(MyDbContext db)
        {
            _db = db;
        }

        [Route("AllProducts")]
        [HttpGet]
        public IActionResult getAllProducts()
        {

            var products = _db.Products.ToList();

            return Ok(products);
        }

        [Route("getProductById/{id}")]
        [HttpGet]
        public IActionResult getProductById(int id)
        {
            var product = _db.Products.Find(id);

            return Ok(product);
        }

        [Route("getProductByName/{name}")]
        [HttpGet]
        public IActionResult getProductByName(string name)
        {
            var product = _db.Products.FirstOrDefault(model => model.ProductName == name);

            return Ok(product);
        }

        [Route("getProductByCategoryId/{id}")]
        [HttpGet]
        public IActionResult getProductByCategoryId(int id)
        {
            var product = _db.Products.Where(model => model.CategoryId == id).ToList();

            return Ok(product);
        }
        [Route("DeleteProduct/{id}")]
        [HttpDelete]
        public IActionResult deleteProduct(int id)
        {
            var delProduct = _db.Products.FirstOrDefault(e => e.ProductId == id);

            if (delProduct != null)
            {
                _db.Products.Remove(delProduct);
                _db.SaveChanges();
                return Ok($"Product deleted successfully.");
            }
            return NotFound($"Product not found.");
        }

        [HttpPost("AddProduct")]
        public IActionResult addProduct([FromForm] ProductRequestDTO product)
        {
            if (product.ProductImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var imagePath = Path.Combine(uploadsFolderPath, product.ProductImage.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    product.ProductImage.CopyToAsync(stream);
                }

            }

            var newProduct = new Product()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ProductImage = product.ProductImage.FileName
            };

            _db.Products.Add(newProduct);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut("UpdateProduct/{id}")]
        public IActionResult UpdateProduct(int id, [FromForm] ProductRequestDTO product)
        {

            if (product.ProductImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var imagePath = Path.Combine(uploadsFolderPath, product.ProductImage.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    product.ProductImage.CopyToAsync(stream);
                }

            }

            var Updateproduct = _db.Products.Find(id);

            Updateproduct.ProductName = product.ProductName;
            Updateproduct.Description = product.Description;
            Updateproduct.Price = product.Price;
            Updateproduct.CategoryId = product.CategoryId;
            Updateproduct.ProductImage = product.ProductImage.FileName;

            _db.Products.Update(Updateproduct);
            _db.SaveChanges();
            return Ok();
        }

    }
}
