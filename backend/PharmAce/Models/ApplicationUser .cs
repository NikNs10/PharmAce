using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PharmAce.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name{get;set;}

        public string Role{get;set;}
    }

}