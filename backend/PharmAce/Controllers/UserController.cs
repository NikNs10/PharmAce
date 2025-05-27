using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;

namespace PharmAce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        public UserController(IUserService user)
        {
            _user = user;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("ViewAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _user.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("View-User/{id}")]
        public async Task<IActionResult> GetUserById(Guid id){
            var user = await _user.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // Add extra features to improve privacy of user data
        [Authorize]
        [HttpPost("Create-User")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null) return BadRequest("User data is null.");
            var res = await _user.CreateUserAsync(userDto);
            if (res) return CreatedAtAction(nameof(GetUserById), new { id = userDto.UserId }, userDto);
            return BadRequest("User creation failed.");
        }

        // For creating a new user we can use the SignUp method in AuthorizeController
        [Authorize]
        [HttpPut("Edit-User/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto userDto){
            if (id != userDto.UserId) return BadRequest("User ID mismatch.");
            var res = await _user.UpdateUserAsync(userDto);
            if (res) return NoContent();
            return NotFound("User not found.");
        }

        [Authorize]
        [HttpDelete("Delete-User/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id){
            var res = await _user.DeleteUserAsync(id);
            if (res) return NoContent();
            return NotFound("User not found.");
        }

    }
}