using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email{get;set;}

        [Required(ErrorMessage = "Password is required.")]
        public string Password{get;set;}
    }
}