using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmAce.Data;
using PharmAce.Models;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;

namespace PharmAce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {
         private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<ApplicationRole> _role;

        private readonly IAuthorizeService _authorize;

        private readonly ApplicationDbContext _context;

        public AuthorizeController(UserManager<ApplicationUser> user ,  RoleManager<ApplicationRole> role , IAuthorizeService authorize , ApplicationDbContext context){
            _user = user ;
            _role = role ;
            _authorize = authorize;
            _context = context ;
        }


        [HttpPost("Sign_up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto){
            var res = await _user.FindByEmailAsync(signUpDto.Email);
            if(res != null){
                return BadRequest("User already exists");
            }
            var User = new ApplicationUser{
                UserName = signUpDto.Email,
                Name=signUpDto.Name,
                Email=signUpDto.Email,
                PhoneNumber=signUpDto.PhoneNumber,
                Role=signUpDto.Role
            };

            var result = await _user.CreateAsync(User , signUpDto.Password);
            if(!result.Succeeded){
                return BadRequest(result.Errors);
            }

            if(!await _role.RoleExistsAsync(signUpDto.Role)){
                await _role.CreateAsync(new ApplicationRole(signUpDto.Role));
            }

            await _user.AddToRoleAsync(User , signUpDto.Role);
            return Ok("User created successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto){
            var res = await _user.FindByEmailAsync(loginDto.Email);
            
            if (res == null || string.IsNullOrEmpty(res.PasswordHash))
            {
                return Unauthorized("User must set a password before login.");
            }

            if (res == null || !await _user.CheckPasswordAsync(res, loginDto.Password))
            {
                return Unauthorized("Invalid email or password");
            }
            var token = await _authorize.GenerateJwtToken(res);
            return Ok(new { Token = token });
        }
    }
}