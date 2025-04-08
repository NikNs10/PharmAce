using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmAce.Models
{
     public enum Role{
        Admin,
        Doctor,
        Supplier
    }
    public class User
    {
        [Key]
        public Guid UserId{get;set;}

        [Required]
        public string Name{get;set;}

        [Required]
        public string Email{get;set;}

        [Required]
        public string PasswordHash{get;set;}


        [Required]
        public Role Role{get;set;}
    }
}
