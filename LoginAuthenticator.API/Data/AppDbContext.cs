using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAuthenticator.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace LoginAuthenticator.API.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){
            
        }
    }
}