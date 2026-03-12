using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new();
        private static int nextId = 1;

   
        [HttpGet]
        public IActionResult GetAll() => Ok(users);

    
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            return user == null ? NotFound(new { message = "User not found" }) : Ok(user);
        }

     
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            user.Id = nextId++;
            users.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User updatedUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound(new { message = "User not found" });

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound(new { message = "User not found" });

            users.Remove(user);
            return NoContent();
        }
    }
}