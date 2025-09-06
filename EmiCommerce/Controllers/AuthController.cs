using EmiCommerce.DTO;
using EmiCommerce.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmiCommerce.Controllers
{
    [ApiController]
    [Route("Register/Login")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Register user 
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var user = await _userService.RegisterAsync(dto);
            return Ok(user);
        }

        // User login 
        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDto dto)
        {
            var user = await _userService.ValidateUserAsync(dto);
            if (user == null)
                return Unauthorized("Invalid user credentials");

            return Ok(user); 
        }

        // Admin login 
        [HttpPost("admin/login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginDto dto)
        {
            var admin = await _userService.AdminLoginAsync(dto);
            if (admin == null)
                return Unauthorized("Invalid admin credentials");

            return Ok(admin); 
        }
    }
}
