using EmiCommerce.DTO;
using EmiCommerce.JWTHelper;
using System.Security.Cryptography;
using System.Text;

namespace EmiCommerce.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<UserDto> RegisterAsync(RegisterUserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("Email already exists");

            var passwordHash = HashPassword(dto.Password);

            var user = UserMapper.ToUserEntity(dto, passwordHash);
            await _userRepository.AddUserAsync(user);

            var profile = UserMapper.ToUserProfileEntity(dto, user.Id);
            await _userRepository.AddUserProfileAsync(profile);

            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Token = null 
            };
        }
        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailOnlyUserAsync(dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return null;

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email, user.Role);

            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };
        }
        
        public async Task<UserDto?> AdminLoginAsync(AdminLoginDto dto)
        {
            var user = await _userRepository.GetByUsernameOrEmailAsync(dto.UsernameOrEmail);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return null;

            // Ensure only admin role can login through this endpoint
            if (user.Role != "Admin")
                return null;

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email, user.Role);

            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };
        }
        
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string entered, string storedHash)
        {
            return HashPassword(entered) == storedHash;
        }
    }
}
