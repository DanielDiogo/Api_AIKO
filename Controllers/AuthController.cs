using Api_AIKO.Interfaces;
using Api_AIKO.Models;
using Api_AIKO.Models.JwtConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_AIKO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationBusinessLogics _authenticationBusinessLogics;

        private readonly IUserRepository _userRepository;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration, IAuthenticationBusinessLogics authenticationBusinessLogics, IUserRepository userRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _authenticationBusinessLogics = authenticationBusinessLogics;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDetail model)
        {
            var userExists = await _userRepository.DoesUserIdExist(model.Id);
            if (userExists)
            {
                // Gera o token JWT usando a classe AuthenticationBusinessLogics
                var token = _authenticationBusinessLogics.GenerateToken(model.Id, _configuration.GetSection("Jwt").Get<JwtConfiguration>());
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }



        [Authorize]
        [HttpPost("saveLoginDetails")]
        public async Task<IActionResult> SaveLoginDetails()
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId != null)
            {
                // await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }
    }
}
