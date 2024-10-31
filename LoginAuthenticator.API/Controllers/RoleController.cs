using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAuthenticator.API.Dtos;
using LoginAuthenticator.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginAuthenticator.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController:ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController (RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager){
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto){
            if(string.IsNullOrEmpty(createRoleDto.RoleName)){
                return BadRequest("Role name is required");
            }

            var RoleExist = await _roleManager.RoleExistsAsync(createRoleDto.RoleName);
            if (RoleExist){
                return BadRequest("Role already exist");
            }

            var RoleResult = await _roleManager.CreateAsync(new IdentityRole(createRoleDto.RoleName));
            if (RoleResult.Succeeded){
                return Ok(new {message="Role created successfully"});
            }
            return BadRequest("Role is not created");
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<RoleResponseDto>>> GetRoles()
        {
            

            // list of roles with total users in each role 

            var roles = await _roleManager.Roles.Select(r=>new RoleResponseDto{
                Id = r.Id,
                Name = r.Name,
                TotalUser = _userManager.GetUsersInRoleAsync(r.Name!).Result.Count
            }).ToListAsync();

            return Ok(roles);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id){
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null){
                return NotFound("Role not found.");
            }

            var result = await _roleManager.DeleteAsync(role);

            if(result.Succeeded){
                return Ok(new {message = "Role deleted successfully"});
            }

            return BadRequest("Role deletion failed");
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] RoleAssignDto roleAssignDto){
            var user = await _userManager.FindByIdAsync(roleAssignDto.UserId);

            if(user is null){
                return NotFound ("User not found.");
            }

            var role = await _roleManager.FindByIdAsync(roleAssignDto.RoleId);

            if (role is null){
                return NotFound ("Role not found.");
            }

            var result = await _userManager.AddToRoleAsync(user,role.Name);

            if(result.Succeeded){
                return Ok(new {message = "Role assigned successfully."});
            }

            var error = result.Errors.FirstOrDefault();

            return BadRequest(error!.Description);
        }
    }
}