using EmiCommerce.DTO;
using EmiCommerce.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmiCommerce.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Register user with role
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                var user = await _userService.RegisterAsync(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Role-based login 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.LoginAsync(dto);
            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok(user); 
        }

        // Admin login with username or email
        [HttpPost("admin-login")]
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginDto dto)
        {
            var admin = await _userService.AdminLoginAsync(dto);
            if (admin == null)
                return Unauthorized("Invalid admin credentials");

            return Ok(admin);
        }
    }
}
