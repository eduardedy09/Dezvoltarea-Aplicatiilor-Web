

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using site.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository userRepo)
        {
            _repo = userRepo;
        }

        // get
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _repo.GetAll();

            return Ok(products);
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await _repo.GetModelById(id);

            if (product != default)
            {
                return Ok(product);
            }
            else
            {
                return BadRequest($"Product with id {id} does not exist");
            }
        }

        // insert
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(
            [FromQuery] string name,
            [FromQuery] string description,
            [FromQuery] string image,
            [FromQuery] decimal price,
            [FromQuery] int categoryId)
        {
            await _repo.Create(name, description, image, price, categoryId);

            return Ok($"Product {name} added");
        }

        // update
        [HttpPut("UpdateProductDescription/{id}")]
        public async Task<IActionResult> UpdateProductDescription(
            [FromRoute] int id, 
            [FromQuery] string newDescription)
        {
            var result = await _repo.UpdateDescription(id, newDescription);

            if (result)
            {
                return Ok("Product updated");
            }
            else
            {
                return BadRequest($"Product with id {id} does not exist");
            }
        }

        [HttpPut("UpdateProductImage/{id}")]
        public async Task<IActionResult> UpdateProductImage(
            [FromRoute] int id,
            [FromQuery] string newImage)
        {
            var result = await _repo.UpdateDescription(id, newImage);

            if (result)
            {
                return Ok("Product updated");
            }
            else
            {
                return BadRequest($"Product with id {id} does not exist");
            }
        }

        [HttpPut("UpdateProductPrice/{id}")]
        public async Task<IActionResult> UpdateProductPrice(
            [FromRoute] int id,
            [FromQuery] decimal newPrice)
        {
            var result = await _repo.UpdatePrice(id, newPrice);

            if (result)
            {
                return Ok("Product updated");
            }
            else
            {
                return BadRequest($"Product with id {id} does not exist");
            }
        }

        // delete
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var result = await _repo.Delete(id);

            if (result)
            {
                return Ok("Product deleted");
            }
            else
            {
                return BadRequest($"Product with id {id} does not exist");
            }
        }
    }
}
