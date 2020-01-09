using AutoMapper;
using AtlantisPortals.API.Entities;
using AtlantisPortals.API.Models;
using AtlantisPortals.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IAgencyRepository _agencyRepo;
        //private readonly IOptions<EmailSettings> _emailSettings;

        public AuthController(
            IConfiguration config,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAgencyRepository agencyRepo
            )
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _agencyRepo = agencyRepo ??
                throw new ArgumentNullException(nameof(agencyRepo));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

        }

        [HttpPost("clientLogin")]
        public async Task<IActionResult> ClientLogin(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userForLoginDto.Email.ToLower());

            if (user == null)
                return Unauthorized();

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {

                if (!user.IsClient)
                    return Unauthorized();

                var userToReturn = _mapper.Map<UserLoginSuccessfulDto>(user);

                return Ok(new
                {
                    token = GenerateJwtToken(user).Result,
                    user = userToReturn
                });
            }

            return Unauthorized();
        }

        [HttpPost("adminLogin")]
        public async Task<IActionResult> AdminLogin(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userForLoginDto.Email.ToLower());

            if (user == null)
                return Unauthorized();

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {

                if (user.IsClient)
                    return Unauthorized();

                var userToReturn = _mapper.Map<UserLoginSuccessfulDto>(user);

                return Ok(new
                {
                    token = GenerateJwtToken(user).Result,
                    user = userToReturn
                });
            }

            return Unauthorized();
        }

        [HttpPost("clientRegister")]
        public async Task<IActionResult> ClientRegister(ClientRegisterDto userRegisterDto)
        {
            //var agency = await _agencyRepo.GetHotelWithCode(userRegisterDto.AgencyId);

            //if (agency == null)
            //    return NotFound($"Could not find agency with Id {userRegisterDto.AgencyId}");

            var userToCreate = _mapper.Map<User>(userRegisterDto);

            userToCreate.IsClient = true;
            userToCreate.UserName = userRegisterDto.Email;
            userToCreate.Created = DateTime.UtcNow;
            userToCreate.LastActive = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(userToCreate, userRegisterDto.Password);

            if (result.Succeeded)
            {
                // set a default role
                var roleResult = await _userManager.AddToRolesAsync(userToCreate, new[] { "Agent" });

                var userToReturn = _mapper.Map<UserDto>(userToCreate);

                return CreatedAtRoute("GetUser",
                    new { controller = "Users", id = userToCreate.Id }, userToReturn);
            }

            return BadRequest(result.Errors);

        }

        [HttpPost("adminRegister")]
        public async Task<IActionResult> AdminRegister(AdminRegisterDto userRegisterDto)
        {

            var userToCreate = _mapper.Map<User>(userRegisterDto);

            userToCreate.IsClient = false;
            userToCreate.UserName = userRegisterDto.Email;
            userToCreate.Created = DateTime.UtcNow;
            userToCreate.LastActive = DateTime.UtcNow;
            userToCreate.AgencyId = 0;

            var result = await _userManager.CreateAsync(userToCreate, userRegisterDto.Password);

            if (result.Succeeded)
            {
                // set a default role
                var roleResult = await _userManager.AddToRolesAsync(userToCreate, new[] { "Admin" });

                var userToReturn = _mapper.Map<UserDto>(userToCreate);

                return CreatedAtRoute("GetUser",
                    new { controller = "Users", id = userToCreate.Id }, userToReturn);
            }

            return BadRequest(result.Errors);

        }
        //[HttpPut("forgotPassword")]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        //{

        //    if (forgotPasswordDto == null)
        //    {
        //        return BadRequest();
        //    }

        //    var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email.ToLower());


        //    if (user == null)
        //    {
        //        return BadRequest("The Email provided is not registered.");
        //    }

        //    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        //    var newPassword = CreateRandomPassword(10);

        //    IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

        //    if (passwordChangeResult.Succeeded)
        //    {
        //        // retrieve Template messages
        //        var templateDetails = await _hotelrepo.FetchMessageTemplate(1, "General.PasswordRecovery", "All");

        //        //get subject of the message
        //        string subject = templateDetails.Subject;

        //        var messageToUse = templateDetails.Body;

        //        messageToUse = messageToUse.Replace("%Guest.Name%", user.FullName);
        //        messageToUse = messageToUse.Replace("%Guest.Email%", user.Email);
        //        messageToUse = messageToUse.Replace("%Guest.Password%", newPassword);

        //        var plainTextContent = Regex.Replace(messageToUse, "<.*?>", String.Empty);

        //        var htmlContent = messageToUse;

        //        EmailSender.ExecuteSingle(_emailSettings.Value.EmailKey, "support@hotelchum.com", "HotelChum",
        //                user.Email, user.FullName, subject, plainTextContent, htmlContent).Wait();

        //        return NoContent();
        //    }

        //    throw new Exception("Failed to reset password.");
        //}
        private string CreateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("JwtSettings:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(Convert.ToInt32(_config.GetSection("JwtSettings:daysToExpiration").Value)),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
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
