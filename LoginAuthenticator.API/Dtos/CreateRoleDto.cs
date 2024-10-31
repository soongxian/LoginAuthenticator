using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAuthenticator.API.Dtos
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage ="Roll name is required")]
        public string RoleName {get; set;} = null!;
    }
}