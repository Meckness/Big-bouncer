using Big_bouncer.Data;
using Big_bouncer.Models;
using Big_bouncer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Big_bouncer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenBuilder _tokenBuilder;

        public AuthenticationController(
            ApplicationDbContext context,
            ITokenBuilder tokenBuilder)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                var dbUser = await _context
            .Users
            .SingleOrDefaultAsync(u => u.Username == user.Username);

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

        [HttpGet("VerifyToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> VerifyToken()
        {
            var username = User
                .Claims
                .SingleOrDefault();

            if (username == null)
            {
                return Unauthorized();
            }

            var userExists = await _context
                .Users
                .AnyAsync(u => u.Username == username.Value);

            if (!userExists)
            {
                return Unauthorized();
            }

            return NoContent();
        }
    }
}
