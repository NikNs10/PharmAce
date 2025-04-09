using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmAce.Models;

namespace PharmAce.Services.Interface
{
    public interface IAuthorizeService
    {
         Task<string> GenerateJwtToken(ApplicationUser user);
    }
}