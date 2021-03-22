using Big_bouncer.Data;
using Big_bouncer.Models;
using Big_bouncer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Big_bouncer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenBuilder _tokenBuilder;
        private readonly UserRepository _userRepository;

        public AuthenticationController(ITokenBuilder tokenBuilder, UserRepository userRepository)
        {
            _tokenBuilder = tokenBuilder;
            _userRepository = userRepository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                var dbUser = await _userRepository.GetUserAsync(user.Username);

                if (dbUser == null)
                {
                    return NotFound("User not found.");
                }

                var isValid = BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password);

                if (!isValid)
                {
                    return BadRequest("Could not authenticate user.");
                }

                var token = _tokenBuilder.BuildToken(user.Username);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "  " + ex.StackTrace);
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] User user)
        {
            try
            {
                var dbUser = await _userRepository.GetUserAsync(user.Username);

                if(dbUser != null)
                {
                    return Problem("User with this username already present", null, 501, null, null);
                }

                EntityEntry<User> userInserted = await _userRepository.AddUserAsync(user);

                if(userInserted == null)
                {
                    return Problem("An error occured while try to Sign In...");
                }
                _userRepository.Save();
                return Ok(JsonConvert.SerializeObject(user));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "  " + ex.StackTrace);
            }
        }

        [HttpGet("VerifyToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> VerifyToken()
        {
            var userClaim = User
                .Claims
                .SingleOrDefault();

            if (userClaim == null)
            {
                return Unauthorized();
            }

            var userExists = await _userRepository.VerifyUserExistance(userClaim);

            if (!userExists)
            {
                return Unauthorized();
            }

            return NoContent();
        }
    }
}
