using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;

namespace PharmAce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController  : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService service){
            _authService=service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto){
            try{
                var token=await _authService.LoginAsync(loginDto);
                return Ok(new{token});
            }
            catch(Exception ex){
                return Unauthorized(new {message=ex.Message});
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto){
            try{
                var result = await _authService.SignUpAsync(signUpDto);
                return Ok(new {message=result});
            }
            catch(Exception ex){
                return BadRequest(new {message=ex.Message});
            }
        }
    }
}