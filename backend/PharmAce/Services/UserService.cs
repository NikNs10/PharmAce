using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmAce.Data;
using PharmAce.Models;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;

namespace PharmAce.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        //private readonly IEmailService _emailService;

        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(u => new UserDto
            {
                UserId = u.Id,
                UserName = u.Name,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role
            });
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            };
        }

        // public async Task<bool> CreateUserAsync(UserDto userDto)
        // {
        //     var user = new Models.ApplicationUser
        //     {
        //         Id = Guid.NewGuid(),
        //         Name = userDto.UserName,
        //         Email = userDto.Email,
        //         UserName = userDto.Email,
        //         PhoneNumber = userDto.PhoneNumber,
        //         Role = userDto.Role
        //     };

        //     _context.Users.Add(user);
        //     return await _context.SaveChangesAsync() > 0;
        // }
    public async Task<bool> CreateUserAsync(UserDto userDto)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Name = userDto.UserName,
            Email = userDto.Email,
            UserName = userDto.Email, // Required by Identity
            PhoneNumber = userDto.PhoneNumber,
            Role = userDto.Role
        };

        // Create user without password
        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
            return false;

        // Assign role
        await _userManager.AddToRoleAsync(user, userDto.Role);

        // Generate password reset token
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebUtility.UrlEncode(token);

        //var resetUrl = $"{_config["FrontendBaseUrl"]}/reset-password?email={user.Email}&token={encodedToken}";

        // Send email
        //await _emailService.SendAsync(user.Email, "Set your password", $"Click to set password: <a href='{resetUrl}'>Reset Password</a>");

        return true;
    }


        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            var user = await _context.Users.FindAsync(userDto.UserId);
            if (user == null) return false;

            user.Name = userDto.UserName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Role = userDto.Role;


            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }   
    }
}