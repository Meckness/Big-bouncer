using Big_bouncer.Data;
using Big_bouncer.Models;
using Big_bouncer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Big_bouncer.BusinessLogic;

namespace Big_bouncer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenBuilder _tokenBuilder;
        private readonly UserBusinessLogic _userBusinessLogic;

        public AuthenticationController(ITokenBuilder tokenBuilder, UserRepository userRepository, UserBusinessLogic userBusinessLogic)
        {
            _tokenBuilder = tokenBuilder;
            _userBusinessLogic = userBusinessLogic;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                var dbUser = await _userBusinessLogic.GetUserAsync(user.Username);

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
                var dbUser = await _userBusinessLogic.GetUserAsync(user.Username);

                if(dbUser != null)
                {
                    return Problem("User with this username already present", null, 501, null, null);
                }

                User userInserted = await _userBusinessLogic.AddUserAsync(user);

                if(userInserted == null)
                {
                    return Problem("An error occured while try to Sign In...");
                }

                await _userBusinessLogic.Save();
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

            var userExists = await _userBusinessLogic.VerifyUserExistance(userClaim);

            if (!userExists)
            {
                return Unauthorized();
            }

            return NoContent();
        }
    }
}
