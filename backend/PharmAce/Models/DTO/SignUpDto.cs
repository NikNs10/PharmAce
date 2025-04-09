using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models.DTO
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name{get;set;}

        [EmailAddress(ErrorMessage = "Email is required.")]
        public string Email{get;set;}

        [Required(ErrorMessage = "Password is required.")]
        public string Password{get;set;}

        [Required]
        [EnumDataType(typeof(Role), ErrorMessage = "Invalid role provided")]
        public string Role{get;set;}

        [Required]
        public string PhoneNumber{get;set;}
    }
}