
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using site.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository userRepo)
        {
            _repo = userRepo;
        }

        // get
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repo.GetAll();

            return Ok(users);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await _repo.GetById(id);

            if (user != default)
            {
                return Ok(user);
            } 
            else
            {
                return BadRequest($"User with id {id} does not exist");
            }
        }

        [HttpGet("GetUserData")]
        public async Task<IActionResult> GetUserData()
        {
            var id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _repo.GetById(id);

            return Ok(user);
        }

        [HttpGet("GetUserOrders/{id}")]
        public async Task<IActionResult> GetUserOrdersById([FromRoute] int id)
        {
            var orders = await _repo.GetUserOrders(id);

            return Ok(orders);
        }

        [HttpGet("GetUserOrders")]
        public async Task<IActionResult> GetUserOrders()
        {
            var id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var orders = await _repo.GetUserOrders(id);

            return Ok(orders);
        }

        // delete
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUserById([FromRoute] int id)
        {
            var result = await _repo.Delete(id);

            if (result)
            {
                return Ok("User deleted");
            }
            else
            {
                return BadRequest($"User with id {id} does not exist");
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            var id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _repo.Delete(id);

            return Ok("User deleted");
        }

        // update
        [HttpPut("UpdatePhone/{id}")]
        public async Task<IActionResult> UpdatePhoneById(
            [FromRoute] int id,
            [FromQuery] string newPhone)
        {
            var result = await _repo.UpdatePhone(id, newPhone);

            return Ok("Phone number updated");
        }

        [HttpPut("UpdateAddress/{id}")]
        public async Task<IActionResult> UpdateAddressById(
            [FromRoute] int id,
            [FromQuery] string newAddress,
            [FromQuery] string newCity,
            [FromQuery] string newCounty,
            [FromQuery] string newCountry,
            [FromQuery] string newZipcode)
        {
            var result = await _repo.UpdateAddress(id, newAddress, newCity, newCounty, newCountry, newZipcode);

            return Ok("Address updated");
        }

        [HttpPut("UpdatePhone")]
        public async Task<IActionResult> UpdatePhone([FromQuery] string newPhone)
        {
            var id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _repo.UpdatePhone(id, newPhone);

            return Ok("Phone number updated");
        }

        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(
            [FromQuery] string newAddress,
            [FromQuery] string newCity,
            [FromQuery] string newCounty,
            [FromQuery] string newCountry,
            [FromQuery] string newZipcode)
        {
            var id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _repo.UpdateAddress(id, newAddress, newCity, newCounty, newCountry, newZipcode);

            return Ok("Address updated");
        }
    }
}
