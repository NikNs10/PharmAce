using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmAce.Data;
using PharmAce.Models;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PharmAce.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<User>  _passwordHasher;

        public AuthService(ApplicationDbContext context,IConfiguration configuration){
            _context=context;
            _configuration=configuration;
            _passwordHasher=new PasswordHasher<User>();
        }

        public async Task<string> LoginAsync(LoginDto loginDto){
            var user = await _context.Users.FirstOrDefaultAsync(p=>p.Email==loginDto.Email);

            if(user==null){
                throw new Exception("Invalid Email or Password");
            }

            var result=_passwordHasher.VerifyHashedPassword(user,user.PasswordHash,loginDto.Password);
            if(result!= PasswordVerificationResult.Success){
                throw new Exception("Invalid Email or Password");
            }

            var claims=new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        public async Task<string> SignUpAsync(SignUpDto signUpDto){
            var userExists=await _context.Users.AnyAsync(p=>p.Email==signUpDto.Email);
            if(userExists){
                throw new Exception("User with this email already exists");
            }

            var newUser= new User{
                Name=signUpDto.Name,
                Email=signUpDto.Email,
                PasswordHash=_passwordHasher.HashPassword(null,signUpDto.Password),
                Role=Enum.Parse<Role>(signUpDto.Role,true)
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return $"{signUpDto.Name} registered successfully";
        }
    }
}