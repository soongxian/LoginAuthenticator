using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAuthenticator.API.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string  Email { get; set; } = string.Empty;

        [Required]
        public string FullName {get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public List<string>? Roles {get; set; }
    }
}