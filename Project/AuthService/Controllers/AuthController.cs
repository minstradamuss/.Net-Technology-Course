using Microsoft.AspNetCore.Mvc;
using AuthService.Models;
using AuthService.Domain;
using AuthService.Infrastructure;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.Authenticate(request.Username, request.Password);
            if (user == null)
                return Unauthorized();

            var token = _tokenService.GenerateToken(user.Username);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var result = _userService.Register(request.Username, request.Password);
            if (!result)
                return BadRequest("User already exists");

            return Ok();
        }
    }
}
