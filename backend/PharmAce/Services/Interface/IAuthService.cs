using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmAce.Models.DTO;

namespace PharmAce.Services.Interface
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto loginDto);
        Task<string> SignUpAsync(SignUpDto signUpDto);
    }
}