using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2Core.DTOfolder;
using Task2Core.Models;

namespace Task2Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CartItemsController(MyDbContext db)
        {
            _db = db;
        }

        [Route("GetToCartItem")]
        [HttpGet]
        public IActionResult Get()
        {
            var cartItem = _db.CartItems.Select(
                x => new CartItemResponseDTO
                {
                    CartItemId = x.CartItemId,
                    CartId = x.CartId,
                    Quantity = x.Quantity,
                    Product = new ProductDTO
                    {
                        ProductId = x.Product.ProductId,
                        ProductName = x.Product.ProductName,
                        Price = x.Product.Price,
                        Description = x.Product.Description,
                    }
                }
                );
            return Ok(cartItem);
        }
        [HttpGet("/GetDataById/{id}")]
        public IActionResult Get(int id)
        {

            var getData = _db.CartItems.Where(l => l.CartId == id).Select(x => new CartItemResponseDTO
            {
                CartId = x.CartId,
                CartItemId = x.CartItemId,
                Quantity = x.Quantity,
                Product = new ProductDTO
                {
                    ProductId = x.Product.ProductId,
                    Price = x.Product.Price,
                    ProductName = x.Product.ProductName,
                    Description = x.Product.Description,

                }


            }).ToList();

            return Ok(getData);
        }
        [HttpPost]
        public IActionResult addCartItem([FromBody] CartItemRequestDTO cart)
        {
            var data = new CartItem
            {
                CartId = cart.CartId,
                Quantity = cart.Quantity,
                ProductId = cart.ProductId,

            };
            _db.CartItems.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            if (id <= 0)
            {
                return BadRequest();

            }
            var product = _db.CartItems.Find(id);
            if (product != null)
            {
                _db.CartItems.Remove(product);
                _db.SaveChanges();
                return NoContent();
            }
            return NotFound();

        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] CartQDTO quantity, int id)
        {


            var item = _db.CartItems.FirstOrDefault(p => p.CartItemId == id);


            item.Quantity = quantity.Quantity;
            _db.CartItems.Update(item);
            _db.SaveChanges();

            return Ok(item);
        }


    }
}
