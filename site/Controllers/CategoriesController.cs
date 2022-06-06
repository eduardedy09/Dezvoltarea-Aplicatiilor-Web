
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repo;

        public CategoriesController(ICategoryRepository categoryRepo)
        {
            _repo = categoryRepo;
        }

        // get
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _repo.GetAll();

            return Ok(categories);
        }

        [HttpGet("GetAllProducts/{id}")]
        public async Task<IActionResult> GetAllProducts([FromRoute] int id)
        {
            var products = await _repo.GetAllProducts(id);

            return Ok(products);
        }

        // insert
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromQuery] string name)
        {
            await _repo.Create(name);

            return Ok($"Category {name} added");
        }

        // update
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(
            [FromRoute] int id, 
            [FromQuery] string newName)
        {
            var result = await _repo.Update(id, newName);

            if (result)
            {
                return Ok("Category updated");
            }
            else
            {
                return BadRequest($"Category with id {id} does not exist");
            }
        }

        // delete
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var result = await _repo.Delete(id);

            if (result)
            {
                return Ok("Category deleted");
            }
            else
            {
                return BadRequest($"Category with id {id} does not exist");
            }
        }
    }
}
