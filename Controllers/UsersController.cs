using AutoMapper;
using AtlantisPortals.API.Entities;
using AtlantisPortals.API.Models;
using AtlantisPortals.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepo,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _userRepo = userRepo ??
                throw new ArgumentNullException(nameof(userRepo));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
        }
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {

            var userFromRepo = await _userRepo.GetUser(id);

            var userToReturn = _mapper.Map<UserDto>(userFromRepo);

            return Ok(userToReturn);
        }
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(PasswordChangeDto passwordChangeDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user,
                    passwordChangeDto.CurrentPassword, passwordChangeDto.NewPassword);

                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("Failed to change password.");
                }
            }
            throw new Exception("Change password failed on save");
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return BadRequest("Could not find user");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (!result.Succeeded)
                return BadRequest("Failed to remove roles");

            result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return NoContent();

            throw new Exception("Failed to delete the user");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;

            selectedRoles ??= new string[] { };
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove the roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("agencyUsers")]
        public async Task<IActionResult> GetAgencyUsers()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _userRepo.GetUser(currentUserId);

            var users = await _userRepo.AgencyUsers(userFromRepo.AgencyId);

            return Ok(users);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("adminUsers")]
        public async Task<IActionResult> GetAdminUsers()
        {
            var users = await _userRepo.AdminUsers();

            return Ok(users);
        }
        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
