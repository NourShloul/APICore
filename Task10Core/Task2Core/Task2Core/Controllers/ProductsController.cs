using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2Core.DTOfolder;
using Task2Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [Authorize]
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


        [Route("getlast5Products")]
        [HttpGet]
        public IActionResult getlastProducts()
        {
            //var products = _db.Products.ToList();
            var products = _db.Products.OrderBy(x => x.ProductName).ToList();
            var p = products.TakeLast(5).ToList();
            if (products == null)
                return NotFound("No products found.");
            return Ok(p);
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

            var product = _db.Products.OrderBy(p => p.ProductName == name).ToList();

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
        //task 4 
        [HttpPost]
        public IActionResult AddProduct([FromForm] productDTO product)
        {


            var uploadImageFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadImageFolder))
            {
                Directory.CreateDirectory(uploadImageFolder);
            }
            var imageFile = Path.Combine(uploadImageFolder, product.ProductImage.FileName);
            using (var stream = new FileStream(imageFile, FileMode.Create))
            {
                product.ProductImage.CopyToAsync(stream);
            }

            var data = new Product
            {
                ProductName = product.ProductName,
                ProductImage = product.ProductImage.FileName,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Description = product.Description,
            };

            _db.Products.Add(data);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult EditProduct([FromBody] productDTO product) 
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadFolder)) 
            {
                Directory.CreateDirectory(uploadFolder);
            }
            var imageFile = Path.Combine(uploadFolder, product.ProductImage.FileName);
            using (var stream = new FileStream(imageFile,FileMode.Create)) 
            {
                product.ProductImage.CopyToAsync(stream);
            }
                return Ok();
        }
    }
}
