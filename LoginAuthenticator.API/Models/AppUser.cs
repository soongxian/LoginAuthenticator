using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LoginAuthenticator.API.Models
{
    public class AppUser : IdentityUser
    {
        public String? FullName { get; set; }
    }
}