using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task2Core.Models;

namespace Task2Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _db;

        public OrdersController(MyDbContext db)
        {
            _db = db;
        }

        [Route("getAllOrders")]
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _db.Orders.Select(b => new
            {
                b.OrderId,
                b.UserId,
                b.OrderDate,
                b.User
            });
            if (orders == null)
                return NotFound("No orders found.");

            return Ok(orders);
        }

        [Route("GetOrderById/{id:int}")]
        [HttpGet]
        public IActionResult GetOrderById(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID.");

            var order = _db.Orders.Find(id);
            if (order == null) return NotFound("Order not found.");

            return Ok(order);
        }

        [Route("GetOrderByName/{name}")]
        [HttpGet]
        public IActionResult GetOrderByName(string name)
        {
            if (name == null) return BadRequest("Name cannot be null");

            var order = _db.Orders.FirstOrDefault(o => o.User.Username == name);
            if (order == null) return NotFound("Order not found.");

            return Ok(order);
        }

        [Route("DeleteOrder/{id}")]
        [HttpDelete]
        public IActionResult DeleteOrder(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID.");

            var order = _db.Orders.Find(id);
            if (order == null) return NotFound("Order not found.");

            _db.Orders.Remove(order);
            _db.SaveChanges();

            return Ok("Order deleted.");
        }
    }
}
