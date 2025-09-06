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

            var user = UserMapper.ToUserEntity(dto, passwordHash, role: "User");
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
        public async Task<UserDto?> ValidateUserAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return null;

            if (user.Role != "User")
                return null;
            var profile = await _userRepository.GetProfileByUserIdAsync(user.Id);
            if (profile == null)
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
        public async Task<UserDto?> AdminLoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailOnlyUserAsync(dto.Email);
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
