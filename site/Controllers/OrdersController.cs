
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using site.DAL.Interfaces;
using site.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repo;

        public OrdersController(IOrderRepository orderRepo)
        {
            _repo = orderRepo;
        }

        // get
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _repo.GetAll();

            return Ok(orders);
        }

        [HttpGet("GetOrder/{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            var order = await _repo.GetModelById(id);

            if (order != default)
            {
                return Ok(order);
            }
            else
            {
                return BadRequest($"Order with id {id} does not exist");
            }
        }

        // insert
        [HttpPost("AddOrder/{userId}")]
        public async Task<IActionResult> AddOrder(
            [FromRoute] int userId,
            [FromBody] List<OrderProductIdModel> products)
        {
            await _repo.Create(userId, products);

            return Ok("Order added");
        }

        // update
        [HttpPut("UpdateOrderStatus/{id}")]
        public async Task<IActionResult> UpdateProductDescription(
            [FromRoute] int id,
            [FromQuery] string newStatus)
        {
            var result = await _repo.UpdateStatus(id, newStatus);

            if (result)
            {
                return Ok("Order status updated");
            }
            else
            {
                return BadRequest($"Order with id {id} does not exist");
            }
        }

        // delete
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var result = await _repo.Delete(id);

            if (result)
            {
                return Ok("Order deleted");
            }
            else
            {
                return BadRequest($"Order with id {id} does not exist");
            }
        }
    }
}
